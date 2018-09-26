﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevAS
{
    [Serializable]
    class MeleeUnits : Unit
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        public int Xpos
        {
            get { return X_position; }
            set { X_position = value; }
        }
        public int Ypos
        {
            get { return Y_position; }
            set { Y_position = value; }
        }
        public int health
        {
            get { return Health; }
            set { Health = value; }
        }
        public int attack
        {
            get { return Attack; }
            set { Attack = value; }
        }
        public int attackRange
        {
            get { return Attack_range; }
            set { Attack_range = value; }
        }
        public int speed
        {
            get { return Speed; }
            set { Speed = value; }
        }
        public int Fact
        {
            get { return Faction; }
            set { Faction = value; }
        }
        public string symbol
        {
            get { return Symbol; }
            set { Symbol = value; }
        }

        public MeleeUnits(int X_position, int Y_position, int Health, int Attack, int Speed, int Attack_range, int Faction, string sym, string name)
        {
            Xpos = X_position;
            Ypos = Y_position;
            health = Health;
            attack = Attack;
            speed = Speed;
            attackRange = Attack_range;
            Fact = Faction;
            symbol = sym;
            name = Name;

        }


        public override void NewMovePos(Direction direction)
        {
            switch (direction)
            {
                case Direction.Nort:
                    {
                        Ypos -= Speed;
                        break;
                    }
                case Direction.East:
                    {
                        Xpos += Speed;
                        break;
                    }
                case Direction.South:
                    {
                        Ypos += Speed;
                        break;
                    }
                case Direction.West:
                    {
                        Xpos -= Speed;
                        break;
                    }
            }


        }
        public override void Combat(Unit u)
        {
            if (u.GetType() == typeof(MeleeUnits))
            {
                Health -= ((MeleeUnits)u).Attack;
            }
            else if (u.GetType() == typeof(RangedUnits))
            {
                Health -= ((RangedUnits)u).attack;
            }

        }
        public override bool AttackRange(Unit u)
        {
            if (u.GetType() == typeof(MeleeUnits))
            {
                MeleeUnits M = (MeleeUnits)u;
                if (DistanceTo(u) <= attackRange)
                {
                    return true;
                }

                //else if (u.GetType() == typeof(RangedUnits))
                //{
                //    Health -= ((RangedUnits)u).attack;
                //}
                else
                {
                    return false;
                }
            }
            return false;
        }
        public override Unit UnitDistance(Unit[] units)
        {
            Unit closest = this;
            int closestDist = 50;
            foreach (Unit u in units)
            {
                if (((MeleeUnits)u).Fact != Fact)
                {
                    if (DistanceTo((MeleeUnits)u) < closestDist)
                    {
                        closest = u;
                        closestDist = DistanceTo((MeleeUnits)u);
                    }
                }
                if (u.GetType() == typeof(MeleeUnits))
                {
                    if (DistanceTo((MeleeUnits)u) < closestDist)
                    {
                        closest = u;
                        closestDist = DistanceTo(u);
                    }
                }
                else if (u.GetType() == typeof(RangedUnits))
                {
                    if (DistanceTo((RangedUnits)u) < closestDist)
                    {
                        closest = u;
                        closestDist = DistanceTo(u);
                    }
                }
            }

            return closest;

        }
        public override bool isDead()
        {
            if (Health < 1)
            {
                return true;
            }
            else

                return false;

        }

        private int DistanceTo(Unit u)
        {
            if (u.GetType() == typeof(MeleeUnits))
            {
                MeleeUnits m = (MeleeUnits)u;
                int d = Math.Abs(Xpos - m.Xpos) + Math.Abs(Ypos - m.Ypos);
                return d;
            }
            else
            {
                return 0;
            }
        }
        public Direction Directionto(Unit u)
        {
            if (u.GetType() == typeof(MeleeUnits))
            {
                MeleeUnits m = (MeleeUnits)u;
                if (m.Xpos < Xpos)
                {
                    return Direction.Nort;
                }
                else if (m.Xpos > Xpos)
                {
                    return Direction.South;
                }
                else if (m.Ypos < Ypos)
                {
                    return Direction.West;
                }
                else
                {
                    return Direction.East;
                }
            }
            else
            {
                return Direction.Nort;
            }

        }

        public override string ToString()
        {

            return "Name: " + Name + "\r\nFaction: " + symbol + "\r\nDamage: " + attack + "\r\nAttackRange: " + attackRange + "\r\nHealth: " + health + "\r\nSpeed: " + speed + "\r\nY postion: " + Xpos + "\r\nX postion: " + Ypos;
        }
        public override void Save()
        {


        }
    }
}
