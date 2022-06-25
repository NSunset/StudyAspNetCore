using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Test.Generic.CovariantAndInversion;
using Test.Generic.Interface;
using Test.Generic.Model;

namespace Test.Generic
{
    public class Entrance
    {
        public Entrance()
        {
            //TestCache();
            //TestConstraint();
            //TestCovariantAndInversion();
        }

        [Test]
        public void TestCache()
        {
            object value = CacheTest<int>.GetCache();

            object value1 = CacheTest<string>.GetCache();

            object value2 = CacheTest<int>.GetCache();

            object value3 = CacheTest<int>.GetCache();

            Assert.AreEqual(value, value2);

        }


        [Test]
        public void TestConstraint()
        {
            IPeople people = new Chinese();

            IPeople hubei = new Hubei();

            Chinese hubei1 = new Hubei();

            Japan japan = new Japan();

            ShanDong shanDong = new ShanDong(10);

            //直接强转
            {
                Constraint.Speak(people);
                Constraint.Speak(hubei);
                Constraint.Speak(hubei1);
                Constraint.Speak(japan);//这里报错
            }

            //基类约束，哪怕传入对象是继承约束的但是声明时是别的类型，这里也不行
            {
                //Constraint.SpeakBaseClassConstraint(people);
                //Constraint.SpeakBaseClassConstraint(hubei);
                Constraint.SpeakBaseClassConstraint(hubei1);
                //Constraint.SpeakBaseClassConstraint(japan);
                //Constraint.SpeakBaseClassConstraint(shanDong);
            }

            //接口约束,传入对象只要继承接口约束就行，声明时是什么不重要
            {
                Constraint.SpeakInterfaceConstraint(people);
                Constraint.SpeakInterfaceConstraint(hubei);
                Constraint.SpeakInterfaceConstraint(hubei1);
                //Constraint.SpeakInterfaceConstraint(japan);
                //Constraint.SpeakInterfaceConstraint(shanDong);
            }

            //无参构造函数约束,声明了构造函数，且不是无参构造函数则无法传入
            {
                //Constraint.NoArgumentConstructorConstraint(people);
                //Constraint.NoArgumentConstructorConstraint(hubei);
                Constraint.NoArgumentConstructorConstraint(hubei1);
                Constraint.NoArgumentConstructorConstraint(japan);
                //Constraint.NoArgumentConstructorConstraint(shanDong);
            }

            //值类型约束,非值类型无法传入
            {
                //Constraint.StructConstraint(people);
                //Constraint.StructConstraint(hubei);
                //Constraint.StructConstraint(hubei1);
                //Constraint.StructConstraint(japan);
                //Constraint.StructConstraint(shanDong);

                Constraint.StructConstraint(10);
                //Constraint.StructConstraint("");
            }

            //引用类型约束，非引用类型无法传入
            {
                Constraint.ClassConstraint(people);
                Constraint.ClassConstraint(hubei);
                Constraint.ClassConstraint(hubei1);
                Constraint.ClassConstraint(japan);
                Constraint.ClassConstraint(shanDong);

                //Constraint.ClassConstraint(10);
                Constraint.ClassConstraint("");
            }
        }

        [Test]
        public void TestCovariantAndInversion()
        {
            GenericList.CovariantTest();
            GenericList.InversionTest();
            GenericList.ListTest();
        }
    }
}
