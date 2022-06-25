using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.Structural
{
    public interface IDbHelp
    {
        void Add<T>(T t);

        void Update<T>(T t);

        void Delete<T>(T t);

        void Find(int id);
    }
}
