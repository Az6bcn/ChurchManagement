﻿namespace Domain.Entities
{
    public class ServiceType
    {
        private ServiceType()
        {

        }

        internal ServiceType(string name)
        {
            Name = name;
        }

        internal ServiceType(int id, string name)
        {
            ServiceTypeId = id;
            Name = name;
        }

        public int ServiceTypeId { get; private set; }
        public string Name { get; private set; }

        public static ServiceType Create(string name) => new(name);
        public static ServiceType Create(int id, string name) => new(id, name);
    }
}