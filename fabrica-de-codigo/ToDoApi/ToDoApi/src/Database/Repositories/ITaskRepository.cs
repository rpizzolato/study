using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApi.src.Database.Domain;

namespace ToDoApi.src.Database.Repositories
{
    public interface ITaskRepository
    {
        IQueryable<Todo> GetAll();
        Todo GetById(Guid Id);
        Todo Save(Todo entity);
    }
}
