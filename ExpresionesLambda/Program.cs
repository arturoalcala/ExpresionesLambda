using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpresionesLambda
{
    class Program
    {
        static void Main(string[] args)
        {
            var beer = new Beer() { Name = "Minerva", Country = "México" };

            //Console.WriteLine(BeerValidations.NotNull(string.Empty));
            //Console.WriteLine(BeerValidations.NotNullName(beer));
            //Console.WriteLine(BeerValidations.NotNullCountry(beer));

            //Console.WriteLine(Validator.Validate(beer, BeerValidations.validations));

            //var func = Validator.Validate(beer, BeerValidations.validations) ? 
            //    (Action) => Console.WriteLine("Éxito") : 
            //    (Action) => Console.WriteLine("Error");

            var func = Validator.Validate(beer, BeerValidations.validations) ? 
                (Action)Success : 
                (Action)Error;

            func();
        }

        public static void Success() => Console.WriteLine("Éxito");
        public static void Error() => Console.WriteLine("Error");

        public class GlobalValidations<T>
        {
            public static readonly Predicate<T> NotNull =
                (d) => d != null;
        }

        public class BeerValidations
        {
            /// <summary>
            /// Predicate significa que valida un boleano
            /// </summary>
            public static readonly Predicate<string> NotNull = (d) => d != null;

            public static readonly Predicate<Beer> NotNullName = (b) => b.Name != null;
            public static readonly Predicate<Beer> NotNullCountry = (b) => b.Country != null;

            public static readonly Predicate<Beer>[] validations =
            {
                (d) => GlobalValidations<string>.NotNull(d.Name),
                (d) => GlobalValidations<string>.NotNull(d.Country),
                (d) => d.Name != null && d.Name.Count() < 10,
                (d) => d.Name != null && d.Country.Count() < 100,
            };
        }

        public class Validator
        {
            public static bool Validate<T>(T beer, params Predicate<T>[] validations) =>
                validations.ToList()
                           .Where(d =>
                           {
                               return !d(beer);
                           })
                           .Count() == 0;
        }

        public class Beer
        {
            public string Name { get; set; }
            public string Country { get; set; }
        }
    }
}
