using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevAS
{
    [Serializable]
    public  class Map
    {
        Random r = new Random();
        int buildingx;
        private ResourceBuilding[] rb;

        public ResourceBuilding[] RB
        {
            get { return rb; }
            set { rb = value; }
        }


        
        
        private Unit[] units;
      

        public Unit[] Units
        {
            get { return units; }
            set { units = value; }
        }

        private Building[] building;

        public Building[] Building
        {
            get { return building; }
            set { building = value; }
        }


        public Map(int maxX, int maxY, int numUnits,int numbuilding) //this creates all the units on the maps 
        {
            building = new Building[numbuilding];
            rb = new ResourceBuilding[numbuilding];
                units = new Unit[numUnits];
            for (int i = 0; i < numUnits; i++)
            {


                if (i <= 10)
                {
                    MeleeUnits M = new MeleeUnits(r.Next(0, maxX), r.Next(0, maxY), r.Next(5, 10) * 10, r.Next(5, 20), 1, 1, i % 2, "}","Knight");
                    Units[i] = M;
                }


                if (i > 10)
                {
                    RangedUnits R = new RangedUnits(r.Next(0, maxX), r.Next(0, maxY), r.Next(5, 10) * 10, r.Next(5, 20), 1, 1, i % 2, "=","Archer");
                    Units[i] = R;
                }
            }
            for (int i = 0; i < numbuilding; i++)
            {


                if (i <= 5)
                {
                    FactoryBuilding fb = new FactoryBuilding(r.Next(0,maxX), r.Next(0,maxY), r.Next(5,20), 1, "F", r.Next(0,1), r.Next(5,10), buildingx);
                    building[i] = fb;
                }


                if (i > 5)
                {
                    FactoryBuilding fbr = new FactoryBuilding(r.Next(0, maxX), r.Next(0, maxY), r.Next(5, 20), 0, "F", r.Next(0, 1), r.Next(5, 10), buildingx);
                    building[i] = fbr;
                }
            }
            for (int i = 0; i < numbuilding; i++)
            {


                if (i <= 5)
                {
                    ResourceBuilding fb = new ResourceBuilding(r.Next(0, maxX), r.Next(0, maxY), r.Next(5, 20), 1, "RB", r.Next(0, 1), r.Next(5, 10), 400);
                    rb[i] = fb;
                }


                if (i > 5)
                {
                    ResourceBuilding fbr = new ResourceBuilding(r.Next(0, maxX), r.Next(0, maxY), r.Next(5, 20), 0, "RB", r.Next(0, 1), r.Next(5, 10),400);
                    rb[i] = fbr;
                }
            }


        }
    }
}
