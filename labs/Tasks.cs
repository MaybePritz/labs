using System;

namespace labs
{
    public class Tasks
    {
        public string[][] TaskObj { get; set; }

        public string GetTaskName(int id)
        {
            foreach (string[] task in TaskObj)
            {
                if (task.Length >= 2 &&
                    int.TryParse(task[0], out int taskId) &&
                    taskId == id)
                {
                    return task[1];
                }
            }
            return "Неизвестное задание";
        }

        public int Reqursion(int M, int N)
        {
            int result;

            if (M == 0) result = 0;
            else
            {
                if (M == 1 || N == 1) result = 1;
                else
                {
                    if (M < N) result = Reqursion(M, M);
                    else
                    {
                        if (M == N) result = 1 + Reqursion(M, M - 1);
                        else result = Reqursion(M, N - 1) + Reqursion(M - N, N);
                    }
                }
            }
            return result;
        }

        public Tasks()
        {
            TaskObj = new string[][]
            {
                new string[] { "0", "Рекурсивный алгоритм Qmxn" },
            };
        }
    }
}
