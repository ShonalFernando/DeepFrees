using DeepFrees.Dispatcher.Model;

namespace DeepFrees.Dispatcher.Microservice
{
    public class TaskAssigner
    {
        public Tuple<List<WorkTask>, List<Technician>> AssignTasks(List<DispatchSolution> DispatchSolutions, List<WorkTask> WorkTasks, List<Technician> Technicians)
        {
            foreach (var DispatchSolution in DispatchSolutions)
            {
                TaskCategory taskCategory = (TaskCategory)DispatchSolution.TaskCategoryID;

                foreach (var Worktask in WorkTasks.Where(wt => wt.isAvailable && wt.taskCategory == taskCategory))
                {
                    // Assignment
                    Worktask.isAvailable = false;

                    AssignedTask assignedTask = new()
                    {
                        dateDay = Worktask.dateDay,
                        dateMonth = Worktask.dateMonth,
                        TaskID = Worktask.taskID
                    };

                    // PointTable
                    foreach (var technician in Technicians)
                    {
                        foreach (var points in technician.WorkTaskPointTable.Where(pt => pt.TaskCategory == DispatchSolution.TaskCategoryID))
                        {
                            points.TaskCategoryPoints += 10;
                        }
                    }
                }
            }

            //DBOperations on Technicians and WorkTasks
            Tuple<List<WorkTask>, List<Technician>> returnable = Tuple.Create(WorkTasks, Technicians);
            return returnable;
        }
    }
}
