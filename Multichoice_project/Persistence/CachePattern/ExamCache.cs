namespace Multichoice_project.Persistence.CachePattern
{
    public class ExamCache
    {
        public DateTime TimeStartTest { get; set; }
        public DateTime TimeEndTest { get; set; }
        public int IdTest { get; set; }
        public int IdUser { get; set; }

        public ExamCache(DateTime timeStartTest, DateTime timeEndTest, int idTest, int idUser)
        {
            this.TimeStartTest = timeStartTest;
            this.TimeEndTest = timeEndTest;
            this.IdTest = idTest;
            this.IdUser = idUser;
        }
    }
}
