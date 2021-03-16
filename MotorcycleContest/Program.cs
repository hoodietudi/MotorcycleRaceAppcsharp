using System;
using MotorcycleContest.model;
using MotorcycleContest.repository;
using MotorcycleContest.repository.interfaces;

namespace MotorcycleContest
{
    class Program
    {
        static void Main(string[] args)
        {
            var UserRepo = new UserDbRepository();

            Console.WriteLine(UserRepo.FilterByUsername("andrei")[0]);
        }
    }
}