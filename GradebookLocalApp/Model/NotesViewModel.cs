using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradebookLocalApp.Model
{
    public class NotesViewModel
    {
        public class ProjectsNotes
        {
            public int id_student { get; set; }

            public int id_proj { get; set; }

            public string StudentName { get; set; }

            public string StudentLastName { get; set; }

            public string? Note { get; set; }




        }

        public class SubjectsNotes
        {
            public int id_student { get; set; }

            public int id_przed { get; set; }

            public string SubjectName { get; set; }

            public string StudentName { get; set; }

            public string StudentLastName { get; set; }

            public string? Note { get; set; }


        }
    }
}
