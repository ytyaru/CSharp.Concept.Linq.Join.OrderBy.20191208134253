using System;
using System.Collections.Generic;
using System.Linq;

namespace Concept.Linq.Lesson0 {
    class Person
    {
        public string Name { get; set; }
    }
    class Pet
    {
        public string Name { get; set; }
        public Person Owner { get; set; }
    }
    class Main {
        private List<Person> persons;
        private List<Pet> pets;
        public void Run() {
            persons = CreatePersons();
            pets = CreatePets();
            Show(Query());
        }
        private List<Person> CreatePersons() {
            return new List<Person>() {
                new Person { Name="A" },
                new Person { Name="B" },
                new Person { Name="C" },
            };
        }
        private List<Pet> CreatePets() {
            return new List<Pet>() {
                new Pet { Name="a", Owner=persons[0] },
                new Pet { Name="b", Owner=persons[0] },
                new Pet { Name="c", Owner=persons[1] },
                new Pet { Name="z", Owner=null },
            };
        }
        private IEnumerable<dynamic> Query() {
            return  from person in persons
                    join pet in pets on person equals pet.Owner into gj
                    orderby person.Name descending
                    select new {
                        person.Name,
                        Pets=from gj2 in gj
                             orderby gj2.Name descending
                             select gj2
                    };
        }
        private void Show(in IEnumerable<dynamic> query) {
            foreach (var item in query) {
                Console.WriteLine($"Person.Name={item.Name}");
                foreach (var pet in item.Pets) {
                    Console.WriteLine($"  Pet.Name={pet.Name}");
                }
            }
        }
    }
}
