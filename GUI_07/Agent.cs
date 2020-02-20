using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AgentAssignment
{
    [Serializable]
    public class Agent : INotifyPropertyChanged
    {
        string id;
        string codeName;
        string speciality;
        string assignment;
        public event PropertyChangedEventHandler PropertyChanged;

        protected void notify([CallerMemberName]string propname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));   
        }


        public Agent()
        {
        }

        public Agent(string aId, string aName, string aSpeciality, string aAssignment)
        {
            id = aId;
            codeName = aName;
            speciality = aSpeciality;
            assignment = aAssignment;
        }

        public string ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public string CodeName
        {
            get
            {
                return codeName;
            }
            set
            {
                codeName = value;
                notify();
            }
        }

        public string Speciality
        {
            get
            {
                return speciality;
            }
            set
            {
                speciality = value;
            }
        }

        public string Assignment
        {
            get
            {
                return assignment;
            }
            set
            {
                assignment = value;
            }
        }


    }
}
