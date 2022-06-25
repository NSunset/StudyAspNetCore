using System;
using System.Collections.Generic;
using System.Text;
using Test.Generic.Interface;
using Test.Generic.Model;

namespace Test.Generic.CovariantAndInversion
{
    public class GenericList
    {
        /// <summary>
        /// 测试泛型集合的声明
        /// </summary>
        public static void ListTest()
        {
            //一个猫是一个动物，有继承关系
            Animal animal = new Cat();

            //一组猫，应该是一组动物才对，但是编译器bug导致编译不通过。
            //因为List<Animal>和List<Cat>没有继承关系，编译器认为是两个不同的类。
            //List<Animal> animals = new List<Cat>();

            //这样就可以了，因为List继承IEnumerable。同时发现IEnumerable指定泛型参数前有out关键字
            //这里就是使用了协变。
            IEnumerable<Animal> animals = new List<Cat>();
        }

        /// <summary>
        /// 测试协变
        /// </summary>
        public static void CovariantTest()
        {
            //定义一个泛型接口和实现类，之间要有继承关系


            //这里是泛型父类声明泛型子类，但是编译器报错
            //ICustomOut<IAnimal> customList = new CustomOut<Cat>();

            //加上协变关键字试试
            //接口上加上协变关键字out 之后可以通过泛型父类声明泛型子类了。
            //因为声明对象是接口，给的泛型参数是Animal
            //所以customList调用接口方法时可以传Anumal，或者Cat
            //但是这里有问题，因为具体的实现类CustomOut的泛型参数指定的是Cat
            //即具体的方法实现只能传Cat。那么这里写的Animal在执行时会报错。类型不匹配
            //问题是，子类出现的地方，使用了父类来代替。

            //为了避免这个问题，所以加上out关键字，让泛型类型不能作为方法参数
            ICustomOut<Animal> customList = new CustomOut<Cat>();
            customList.GetValue();
            //customList.SetValue(new Animal());
            //customList.SetValue(new Cat());
        }

        /// <summary>
        /// 测试逆变
        /// </summary>
        public static void InversionTest()
        {
            //定义一个泛型接口和实现类，之间要有继承关系


            //这里是泛型子类声明泛型父类，但是编译器报错
            //ICustomIn<Cat> customList = new CustomIn<Animal>();

            //加上逆变关键字试试
            //接口上加上逆变关键字in 之后可以通过泛型子类声明泛型父类了。
            //因为声明对象是接口，给的泛型参数是Cat
            //所以customList接口方法返回时只能返回Cat

            //但是这里有问题，因为具体的实现类CustomIn的泛型参数指定的是Animal
            //即具体的方法实现返回的可能是Animal或者Cat。那么在具体的实现类CustomIn的方法中一旦返回Animal
            //在执行时会报错。类型不匹配
            //问题是，子类出现的地方，使用了父类来代替。

            //为了避免这个问题，所以加上in关键字，让泛型类型不能作为方法返回值
            ICustomIn<Cat> customList = new CustomIn<Animal>();
            customList.SetValue(new Cat());

            //customList.GetValue();
        }
    }
}
