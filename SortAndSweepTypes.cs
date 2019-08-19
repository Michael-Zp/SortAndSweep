using System.Collections.Generic;
using System.Numerics;

namespace SortAndSweepTypes
{

    public enum MarkerType
    {
        Min, Max
    }
    public class Marker
    {
        public float Value { get; set; }
        public MarkerType Type { get; set; }

        public Marker(float value, MarkerType type)
        {
            Value = value;
            Type = type;
        }
    }



    class SASItem
    {
        public float Value;
        public MarkerType Type;
        public readonly int Id;

        public SASItem MinItem;
        
        private static int _nextId { get; set; }
        
        private readonly Marker _marker;
        
        public void UpdateValues()
        {
            Value = _marker.Value;
        }

        private static List<SASItem> CreateItem(Marker minMarker, Marker maxMarker)
        {
            int Id = _nextId;
            _nextId++;

            SASItem minItem = new SASItem(minMarker, Id);


            return new List<SASItem>() { minItem, new SASItem(maxMarker, Id, minItem) };
        }

        private SASItem(Marker marker, int id)
        {
            Id = id;

            _marker = marker;
            Type = marker.Type;
            MinItem = this;

            UpdateValues();
        }

        private SASItem(Marker marker, int id, SASItem minItem) : this(marker, id)
        {
            MinItem = minItem;
        }

        public static List<SASItem> CreateItem(float min, float max)
        {
            return CreateItem(new Marker(min, MarkerType.Min), new Marker(max, MarkerType.Max));
        }

        public static List<SASItem> CreateItem(double min, double max)
        {
            return CreateItem(new Marker((float)min, MarkerType.Min), new Marker((float)max, MarkerType.Max));
        }
    }


    //class MySASList
    //{
    //    public MySASListItem First;
    //    public MySASListItem Last;

    //    public void AddLast(SASItem item)
    //    {
    //        if(First == null)
    //        {
    //            MySASListItem listItem = new MySASListItem(null, item, this);
    //            First = listItem;
    //            Last = listItem;
    //        }
    //        else
    //        {
    //            MySASListItem listItem = new MySASListItem(Last, item, this);
    //            Last.Next = listItem;
    //        }
    //    }
    //}

    //class MySASListItem
    //{
    //    public MySASListItem Before;
    //    public SASItem This;
    //    public MySASListItem Next;

    //    public MySASList List;

    //    public MySASListItem(MySASListItem before, SASItem @this, MySASList list)
    //    {
    //        Before = before;

    //        if (Before != null)
    //        {
    //            Before.Next = this;
    //        }

    //        This = @this;
    //        Next = null;

    //        List = list;
    //    }

    //    public void RemoveThis()
    //    {
    //        if (Before != null)
    //        {
    //            Before.Next = Next;
    //        }

    //        if (Next != null)
    //        {
    //            Next.Before = Before;
    //        }

    //        if(this == List.First)
    //        {
    //            List.First = Next;
    //        }

    //        if(this == List.Last)
    //        {
    //            List.Last = Before;
    //        }
    //    }
    //}
}
