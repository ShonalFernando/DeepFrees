using DeepFrees.Dispatcher.Model;

namespace DeepFrees.Dispatcher.Microservice
{
    public class TaskAssigner
    {
        public Tuple<List<WorkTask>, List<Technician>> AssignTasks(List<DispatchSolution> DispatchSolutions, List<WorkTask> WorkTasks, List<Technician> Technicians)
        {
            foreach (var DispatchSolution in DispatchSolutions) //234234245 : 5
            {
                TaskCategory taskCategory = (TaskCategory)DispatchSolution.TaskCategoryID;

                foreach (var Worktask in WorkTasks.Where(wt => wt.isAvailable && !wt.isCompleted && wt.taskCategory == taskCategory))
                {

                    AssignedTask assignedTask = new()
                    {
                        dateDay = Worktask.dateDay,
                        dateMonth = Worktask.dateMonth,
                        TaskID = Worktask.taskID
                    };

                    Console.WriteLine("hooooooooooooooooooo");
                    // Assignment
                    Worktask.isAvailable = false;

                    //First Come First Server
                    Technicians.Find(t => t.NIC.Equals(DispatchSolution.EmployeeID)).AssignedTasks.Add(assignedTask);

                    // PointTable
                    Technician technician = Technicians.Find(t => t.NIC.Equals(DispatchSolution.EmployeeID));
                        foreach (var points in technician.WorkTaskPointTable.Where(pt => pt.TaskCategory == DispatchSolution.TaskCategoryID))
                        {
                            points.TaskCategoryPoints += 10; //Formula
                        }
                }

            }

            //DBOperations on Technicians and WorkTasks
            Tuple<List<WorkTask>, List<Technician>> returnable = Tuple.Create(WorkTasks, Technicians);
            return returnable;
        }
    }
}
