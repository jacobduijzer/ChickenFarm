using System;
using System.Collections.Generic;
using System.Linq;
using ChickenFarm.TaskService.Domain;

namespace ChickenFarm.TaskService.Infrastructure
{
    public static class DatabaseSeeder
    {
        public static void AddTestData(this TaskDbContext dbContext)
        {
            if (!dbContext.Farms.Any())
            {
                dbContext.Farms.Add(new Farm
                {
                    Sheds = new List<Shed>
                    {
                        new Shed
                        {
                            Tasks = new List<Task>
                            {
                                new Task {DateTime = DateTime.Now.AddDays(-1)},
                                new Task {DateTime = DateTime.Now.AddDays(-2)},
                                new Task {DateTime = DateTime.Now.AddDays(-3)},
                                new Task {DateTime = DateTime.Now.AddDays(-4)},
                                new Task {DateTime = DateTime.Now.AddDays(-5)},
                            }
                        },
                        new Shed
                        {
                            Tasks = new List<Task>
                            {
                                new Task {DateTime = DateTime.Now.AddDays(-1)},
                                new Task {DateTime = DateTime.Now.AddDays(-2)},
                                new Task {DateTime = DateTime.Now.AddDays(-3)},
                                new Task {DateTime = DateTime.Now.AddDays(-4)},
                                new Task {DateTime = DateTime.Now.AddDays(-5)},
                            }
                        },
                        new Shed
                        {
                            Tasks = new List<Task>
                            {
                                new Task {DateTime = DateTime.Now.AddDays(-1)},
                                new Task {DateTime = DateTime.Now.AddDays(-2)},
                                new Task {DateTime = DateTime.Now.AddDays(-3)},
                                new Task {DateTime = DateTime.Now.AddDays(-4)},
                                new Task {DateTime = DateTime.Now.AddDays(-5)},
                            }
                        },
                        new Shed
                        {
                            Tasks = new List<Task>
                            {
                                new Task {DateTime = DateTime.Now.AddDays(-1)},
                                new Task {DateTime = DateTime.Now.AddDays(-2)},
                                new Task {DateTime = DateTime.Now.AddDays(-3)},
                                new Task {DateTime = DateTime.Now.AddDays(-4)},
                                new Task {DateTime = DateTime.Now.AddDays(-5)},
                            }
                        }
                    }
                });
                dbContext.SaveChanges();
            }
        }
    }
}