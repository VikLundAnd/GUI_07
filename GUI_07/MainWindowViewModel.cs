using AgentAssignment;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace GUI_07
{
    public class MainWindowViewModel : BindableBase
    {
        string _filename = null;
        string _availableFiletypes = "xml files (*.xml)|*.xml";
        int current_index = 0;
        DelegateCommand add_new_agent;
        DelegateCommand _new;
        DelegateCommand delete_new_agent;
        DelegateCommand _forward;
        DelegateCommand _backward;
        DelegateCommand _save_as;
        DelegateCommand _save;
        DelegateCommand _open;
        DelegateCommand _exit;

        ObservableCollection<Agent> _listAgent = new ObservableCollection<Agent>();
        Agent _currentAgent = null;



        public MainWindowViewModel()
        {
            generateList();
        }

        public string Filename
        {
            get => _filename;
            private set => SetProperty(ref _filename, value);
        }
        public Agent currentAgent
        {
            get => _currentAgent;
            set => SetProperty(ref _currentAgent, value);
        }

        public int CurrentIndex
        {
            get => current_index;
            set => SetProperty(ref current_index, value);
        }

        public ObservableCollection<Agent> listAgent
        {
            get => _listAgent;
            private set => SetProperty(ref _listAgent, value);
        }

        public DelegateCommand AddNewAgent
        {
            get
            {
                return add_new_agent ?? (add_new_agent = new DelegateCommand(() => listAgent.Add(new Agent("---", "---", "---", "---"))));
            }
        }
        public DelegateCommand DeleteNewAgent
        {
            get
            {
                return delete_new_agent ?? (delete_new_agent = new DelegateCommand(() =>
                {
                    if (CurrentIndex >= 0) listAgent.RemoveAt(CurrentIndex);
                }, 
                () => CurrentIndex >= 0 ).ObservesProperty(() => CurrentIndex));
            }
        }

        public DelegateCommand Exit
        {
            get
            {
                return _exit ?? (_exit = new DelegateCommand(() => System.Windows.Application.Current.Shutdown()));
            }
        }

        public DelegateCommand Forward
        {
            get
            {
                return _forward ?? (_forward= new DelegateCommand(
                    ()=> CurrentIndex++, 
                    ()=> (CurrentIndex >= listAgent.Count - 1 ? false : true))
                    .ObservesProperty(()=> CurrentIndex).ObservesProperty(() => listAgent.Count));
            }
        }

        public DelegateCommand Backward
        {
            get
            {
                return _backward ?? (_backward = new DelegateCommand(
                    () => CurrentIndex--,
                    () => (current_index >= 1 ? true : false))
                    .ObservesProperty(() => CurrentIndex));
            }
        }

        public DelegateCommand SaveAs
        {
            get
            {
                return _save_as ?? (_save_as = new DelegateCommand(
                    SaveAsExecute));
            }
        }

        public DelegateCommand Save
        {
            get
            {
                return _save ?? (_save = new DelegateCommand(
                    SaveExecute, () => Filename != null).ObservesProperty(() => Filename));
            }
        }

        public DelegateCommand Open
        {
            get
            {
                return _open ?? (_open = new DelegateCommand(
                    OpenExecute));
            }
        }

        public DelegateCommand New
        {
            get
            {
                return _new ?? (_new = new DelegateCommand(
                    () => {
                        Filename = null;
                        listAgent.Clear();
                        }));
            }
        }
        private void SaveAsExecute()
        {
            XmlSerializer XML_serial = new XmlSerializer(typeof(ObservableCollection<Agent>));

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = _availableFiletypes;
            saveFileDialog1.ShowDialog();
            Filename = saveFileDialog1.FileName;

            TextWriter writer = new StreamWriter(Filename);

            XML_serial.Serialize(writer, listAgent);
            
            writer.Close();
        }

        private void SaveExecute()
        {
            XmlSerializer XML_serial = new XmlSerializer(typeof(ObservableCollection<Agent>));

            TextWriter writer = new StreamWriter(Filename);

            XML_serial.Serialize(writer, listAgent);

            writer.Close();
        }

        private void OpenExecute()
        {
            XmlSerializer XML_serial = new XmlSerializer(typeof(ObservableCollection<Agent>));
            

            OpenFileDialog OpenFileDialog1 = new OpenFileDialog();
            OpenFileDialog1.Filter = _availableFiletypes;
            OpenFileDialog1.ShowDialog();
            
            Filename = OpenFileDialog1.FileName;

            FileStream fs = new FileStream(Filename, FileMode.Open);

            

            listAgent = (ObservableCollection<Agent>) XML_serial.Deserialize(fs);

            fs.Close();
        }

        void generateList()
        {
            listAgent.Add(new Agent("007", "James Bond", "Assasination", "UpperVolta"));
            listAgent.Add(new Agent("001", "Nina", "Assasination", "UpperVolta"));
        }
    }
}
