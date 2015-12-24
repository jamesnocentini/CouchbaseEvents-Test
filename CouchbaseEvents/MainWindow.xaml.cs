using Couchbase.Lite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CouchbaseEvents
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string DB_NAME = "couchbaseevents";
        const string SYNC_URL = "http://syncgateway-1.kitchen-sync-staging.jamiltz.cont.tutum.io:4984/recette/";
        const string TAG = "CouchbaseEvents";
        Database db;
        public MainWindow()
        {
            InitializeComponent();
            Debug.WriteLine("Begin Couchbase Events App", TAG);
            HelloCBL();
            startReplications();
            Debug.WriteLine("End Couchbase Events App", TAG);
        }

        void HelloCBL()
        {
            try
            {
                db = Couchbase.Lite.Manager.SharedInstance.GetDatabase(DB_NAME);
            }
            catch (Exception e)
            {
                Debug.WriteLine("{0}: Error getting database: {1}", TAG, e.Message);
                return;
            }
        }

        void startReplications()
        {
            Replication pull = db.CreatePullReplication(new Uri(SYNC_URL));
            pull.Start();

            pull.Changed += (sender, e) =>
            {
                Console.WriteLine("Repliaction status " + pull.Status);
            };
        }
    }
}
