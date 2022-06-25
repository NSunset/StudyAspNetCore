using Hangfire;
using Hangfire.Client;
using Hangfire.Common;
using Hangfire.Server;
using Hangfire.Storage;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.LiveBackgroundManage.HangfireService.Filter
{
    /// <summary>
    /// 禁用多个排队项目
    /// <remarks>同个任务取消并行执行，期间进来的任务不会等待，会被取消</remarks>
    /// </summary>
    public class DisableMultipleQueuedItemsFilter : JobFilterAttribute, IClientFilter, IServerFilter
    {
        /// <summary>
        /// 单位秒
        /// </summary>
        private const int lockTimeout = 5;

        /// <summary>
        /// 单位小时
        /// </summary>
        private const int fingerprintTimeout = 1;//任务执行超时时间


        public void OnCreating(CreatingContext filterContext)
        {
            if (!AddFingerprintIfNotExists(
                filterContext.Connection,
                GetFingerprintKey(filterContext.Job)
                ))
            {
                filterContext.Canceled = true;
            }
        }

        public void OnPerformed(PerformedContext filterContext)
        {
            RemoveFingerprint(
                filterContext.Connection,
                GetFingerprintKey(filterContext.BackgroundJob.Job)
            );
        }

        private static bool AddFingerprintIfNotExists(IStorageConnection connection, string fingerprintKey)
        {
            using (connection.AcquireDistributedLock(GetFingerprintLockKey(fingerprintKey), TimeSpan.FromSeconds(lockTimeout)))
            {
                var servers= JobStorage.Current.GetMonitoringApi().Servers().ToList();
                var storage= servers.OrderByDescending(x => x.Heartbeat).FirstOrDefault();

                var fingerprint = connection.GetAllEntriesFromHash(GetFingerprintKey(fingerprintKey));

                if (fingerprint != null &&
                    fingerprint.ContainsKey("Timestamp") &&
                    DateTimeOffset.TryParse(
                        fingerprint["Timestamp"],
                        null,
                        DateTimeStyles.RoundtripKind,
                        out var timestamp) &&
                    DateTimeOffset.UtcNow <= timestamp.Add(TimeSpan.FromHours(fingerprintTimeout)) &&
                    fingerprint.ContainsKey("ServerName") &&
                    fingerprint["ServerName"] == storage.Name
                )
                {
                    // 有任务还未执行完，并且没有超时
                    return false;
                }

                // 没有任务执行，或者该任务已超时
                connection.SetRangeInHash(
                    GetFingerprintKey(fingerprintKey),
                    new Dictionary<string, string>
                    {
                        {
                            "Timestamp", DateTimeOffset.UtcNow.ToString("o")
                        },
                        {
                            "ServerName", storage.Name
                        }
                    }
                );

                return true;
            }
        }

        private static void RemoveFingerprint(IStorageConnection connection, string recurringJobId)
        {
            using (connection.AcquireDistributedLock(GetFingerprintLockKey(recurringJobId), TimeSpan.FromSeconds(lockTimeout)))
            using (var transaction = connection.CreateWriteTransaction())
            {
                transaction.RemoveHash(GetFingerprintKey(recurringJobId));
                transaction.Commit();
            }
        }

        private string GetFingerprintKey(Job job)
        {
            string fingerprintKey = $"{job.Type.FullName}.{job.Method.Name}";
            return fingerprintKey;
        }

        private static string GetFingerprintLockKey(string key)
        {
            return String.Format("{0}:lock", key);
        }

        private static string GetFingerprintKey(string key)
        {
            return String.Format("fingerprint:{0}", key);
        }


        void IClientFilter.OnCreated(CreatedContext filterContext)
        {

        }

        void IServerFilter.OnPerforming(PerformingContext filterContext)
        {
        }

    }
}
