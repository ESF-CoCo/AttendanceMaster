﻿using Server.Models;

namespace Server.Interfaces
{
    public interface IRepository<Model>
    {
        public List<Model> allObjs { get; set; }

        public List<Model> Get();

        public Model GetByID(int id);

        public bool Save();
    }
}