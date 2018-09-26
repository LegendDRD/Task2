using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
namespace GameDevAS
{
    [Serializable]
    public partial class Form1 : Form
    {
        int Turn = 0;
        Random r = new Random();
        Map map = new Map(20, 20, 20,10); // this sets the size of the x and y as well as the unit size and how many buildings there are

        const int spacing = 20;
        const int Size = 20;
        public Form1()
       {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateMap(); // this updates the map
            DisplayMap(); // this displays the map
            textBox1.Text = (++Turn).ToString(); // this changes the timer
        }
        private void DisplayMap() // this fills out the entire map with units
        {
            groupBox1.Controls.Clear(); // this clears the buttons and addes new ones 

            foreach (Unit u in map.Units)
            {
                if (u.GetType() == typeof(MeleeUnits))
                {
                    int start_x = 20;
                    int start_Y = 20;
                    start_x = groupBox1.Location.X;
                    start_Y = groupBox1.Location.Y;
                    MeleeUnits m = (MeleeUnits)u;
                    Button t = new Button();

                    t.Size = new Size(Size, Size);
                    t.Location = new Point(start_x + (m.Xpos * Size), start_Y + (m.Ypos * Size));

                    if (m.Fact == 1)
                    {
                        t.Text = "}";
                        t.ForeColor = Color.Blue;

                    }
                    else
                    {
                        t.Text = "}";
                        t.ForeColor = Color.Red;
                    }
                    if (m.isDead())
                    {
                        t.Text = "X";
                    }
                    groupBox1.Controls.Add(t);
                   t.Click += new EventHandler(Buttonunit_Click);
                }

            }
            foreach (Unit u in map.Units)
            {
                if (u.GetType() == typeof(RangedUnits))
                {
                    int start_x = 20;
                    int start_Y = 20;
                    start_x = groupBox1.Location.X;
                    start_Y = groupBox1.Location.Y;
                    RangedUnits r = (RangedUnits)u;
                    Button b = new Button();

                    b.Size = new Size(Size, Size);
                    b.Location = new Point(start_x + (r.Xpos * Size), start_Y + (r.Ypos * Size));



                    if (r.Fact == 1)
                    {

                        b.Text = "=";
                        b.ForeColor = Color.Blue;

                    }
                    else
                    {

                        b.Text = "=";
                        b.ForeColor = Color.Red;
                    }
                    if (r.isDead())
                    {
                        b.Text = "X";
                    }
                    groupBox1.Controls.Add(b);
                    b.Click += new EventHandler(Buttonunit_Click);
                }

            }
            foreach (Building b in map.Building)
            {
                if (b.GetType() == typeof(FactoryBuilding))
                {
                    int start_x = 20;
                    int start_Y = 20;
                    start_x = groupBox1.Location.X;
                    start_Y = groupBox1.Location.Y;
                    FactoryBuilding m = (FactoryBuilding)b;
                    Button a = new Button();

                    a.Size = new Size(Size, Size);
                    a.Location = new Point(start_x + (m.Xpos * Size), start_Y + (m.Ypos * Size));

                    if (m.Fact == 1)
                    {
                        a.Text = "F";
                        a.ForeColor = Color.Blue;

                    }
                    else
                    {
                        a.Text = "F";
                        a.ForeColor = Color.Red;
                    }
                    if (m.isDestoryed())
                    {
                        a.Text = "X";
                    }
                    groupBox1.Controls.Add(a);
                    a.Click += new EventHandler(Buttonunit_Click1);
                }

            }
            foreach (ResourceBuilding b in map.RB)
            {
                if (b.GetType() == typeof(ResourceBuilding))
                {
                    int start_x = 20;
                    int start_Y = 20;
                    start_x = groupBox1.Location.X;
                    start_Y = groupBox1.Location.Y;
                    ResourceBuilding m = (ResourceBuilding)b;
                    Button a = new Button();

                    a.Size = new Size(Size, Size);
                    a.Location = new Point(start_x + (m.Xpos * Size), start_Y + (m.Ypos * Size));

                    if (m.Fact == 1)
                    {
                        a.Text = "RB";
                        a.ForeColor = Color.Blue;

                    }
                    else
                    {
                        a.Text = "RB";
                        a.ForeColor = Color.Red;
                    }
                    if (m.isDestoryed())
                    {
                        a.Text = "X";
                    }
                    groupBox1.Controls.Add(a);
                    a.Click += new EventHandler(Buttonunit_Click2);
                }

            }
        }
        private void UpdateMap() // this updates the map every thick by choooing the direction of which a unit will go when its health is below 25
        {

            foreach (Unit u in map.Units)
            {
                if (u.GetType() == typeof(MeleeUnits))
                {
                    MeleeUnits m = (MeleeUnits)u;
                    if (m.health >1) {

                        if (m.health < 25)
                        {
                            switch (r.Next(0, 4))
                            {
                                case 0: ((MeleeUnits)u).NewMovePos(Direction.Nort); break;
                                case 1: ((MeleeUnits)u).NewMovePos(Direction.East); break;
                                case 2: ((MeleeUnits)u).NewMovePos(Direction.South); break;
                                case 3: ((MeleeUnits)u).NewMovePos(Direction.West); break;

                            }
                        }
                        else
                        {
                            bool inCombat = false;
                            foreach (Unit e in map.Units)
                            {

                                if (u.AttackRange(e))
                                {
                                    u.Combat(e);
                                    inCombat = true;
                                }
                            }
                            if (!inCombat)
                            {
                                Unit c = u.UnitDistance(map.Units);
                                m.NewMovePos(m.Directionto(c));
                            }

                        }
                    }

                }

            }
            foreach (Unit u in map.Units)
            {
                if (u.GetType() == typeof(RangedUnits))
                {
                    RangedUnits ra = (RangedUnits)u;
                    if (ra.health>1) {
                        if (ra.health < 25)
                        {
                            switch (r.Next(0, 4))
                            {
                                case 0: ((RangedUnits)u).NewMovePos(Direction.Nort); break;
                                case 1: ((RangedUnits)u).NewMovePos(Direction.East); break;
                                case 2: ((RangedUnits)u).NewMovePos(Direction.South); break;
                                case 3: ((RangedUnits)u).NewMovePos(Direction.West); break;

                            }
                        }
                        else
                        {
                            bool inCombat = false;
                            foreach (Unit e in map.Units)
                            {

                                if (u.AttackRange(e))
                                {
                                    u.Combat(e);
                                    inCombat = true;
                                }
                            }
                            if (!inCombat)
                            {
                                Unit c = u.UnitDistance(map.Units);
                                ra.NewMovePos(ra.Directionto(c));
                            }

                        }
                    }

                }

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateMap(); // this updates and displays the make every tick of the game
            DisplayMap();
            textBox1.Text = (++Turn).ToString();
           
        }
        public void Buttonunit_Click(object sender, EventArgs args)
        {
            int y = (((Button)sender).Location.Y - groupBox1.Location.Y) / Size; // this will get the x and y postion of the button and match it will a unit with the same x and y postion
                                                                                 //this will will help with displaying the units information
            int x = (((Button)sender).Location.X - groupBox1.Location.X) / Size;
            textBox2.Text = ((Button)sender).Location.ToString();

            foreach (Unit u in map.Units)
            {
                if (u.GetType()== typeof (RangedUnits))
                {
                    RangedUnits r = (RangedUnits)u;
                    if (r.Xpos == x && r.Ypos ==y)
                    {
                        textBox2.Text = r.ToString();

                    }
                }else if (u.GetType() == typeof(MeleeUnits))
                {
                    MeleeUnits m = (MeleeUnits)u;
                    if (m.Xpos == x && m.Ypos == y)
                    {
                        textBox2.Text = m.ToString();
                    }
                }
            }


        }
        public void Buttonunit_Click1(object sender, EventArgs args)
        {
            int y = (((Button)sender).Location.Y - groupBox1.Location.Y) / Size; //this will make the x and y of the button to the builidng and display the building infmoration
            int x = (((Button)sender).Location.X - groupBox1.Location.X) / Size;
            textBox2.Text = ((Button)sender).Location.ToString();

            foreach (FactoryBuilding u in map.Building)
            {
                if (u.GetType() == typeof(FactoryBuilding))
                {
                    FactoryBuilding r = (FactoryBuilding)u;
                    if (r.Xpos == x && r.Ypos == y)
                    {
                        textBox2.Text = r.toString();

                    }
                }
                else if (u.GetType() == typeof(FactoryBuilding))
                {
                    FactoryBuilding m = (FactoryBuilding)u;
                    if (m.Xpos == x && m.Ypos == y)
                    {
                        textBox2.Text = m.toString();
                    }
                }
            }


        }
        public void Buttonunit_Click2(object sender, EventArgs args) // this will use the x and y postions of the button clicked to get the same x and y of a resource building and display its information
        {
            int y = (((Button)sender).Location.Y - groupBox1.Location.Y) / Size;
            int x = (((Button)sender).Location.X - groupBox1.Location.X) / Size;
            textBox2.Text = ((Button)sender).Location.ToString();

            foreach (ResourceBuilding u in map.RB)
            {
                if (u.GetType() == typeof(ResourceBuilding))
                {
                    ResourceBuilding r = (ResourceBuilding)u;
                    if (r.Xpos == x && r.Ypos == y)
                    {
                        textBox2.Text = r.toString();

                    }
                }
                else if (u.GetType() == typeof(ResourceBuilding))
                {
                    ResourceBuilding m = (ResourceBuilding)u;
                    if (m.Xpos == x && m.Ypos == y)
                    {
                        textBox2.Text = m.toString();
                    }
                }
            }


        }

        private void Start_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true; // this starts the timer
        }

        private void Pause_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false; // this stops the timer
        }

        private void savebtm_Click(object sender, EventArgs e)
        {

            SaveMap(); // this saves the maps imforation

           
            
        }

        private void loadbtm_Click(object sender, EventArgs e)
        {

            LoadMap(); // this loads all the imformation
            
        }
        public void SaveMap()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fsout = new FileStream("Save.dat", FileMode.Create, FileAccess.Write, FileShare.None);


            try
            {
                using (fsout)
                {
                    bf.Serialize(fsout, map);
                    
                    MessageBox.Show("Info Saved");


                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message);

            }
        }
        public void LoadMap()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fsin = new FileStream("Save.dat", FileMode.Open, FileAccess.Read, FileShare.None);

            try
            {
                using (fsin)
                {

                    map = (Map)bf.Deserialize(fsin);

                    MessageBox.Show("Save Loaded");
                }
            }

            catch (Exception ea)
            {
                MessageBox.Show("Error" + ea);
            }

            UpdateMap();
        }
    }
}
