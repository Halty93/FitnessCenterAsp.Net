using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Model
{
    public class Activity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Difficultness { get; set; }
        //pridat trenera jako zakladatele kurzu

        public static List<Activity> FakeList
        {
            get
            {
                List<Activity> acts = new List<Activity>();
                acts.Add(new Activity() { Id = 1, Name = "Kruhový trénink", Description = "Pro lidi, kteri chteji zhubnout", Difficultness = 3 });
                acts.Add(new Activity() { Id = 1, Name = "Trampoliny", Description = "Skupinove skakani na trampolinach", Difficultness = 2 });
                acts.Add(new Activity() { Id = 1, Name = "TRX", Description = "Cviceni s TRX", Difficultness = 6 });
                return acts;
            }
        }
    }
}
