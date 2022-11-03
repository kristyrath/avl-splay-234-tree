using System;
namespace As5
{
    public class SortableObject: IComparable
    {
        private int value;
        public SortableObject(int v)
        {
            value = v; 
        }
        public int Value
        {
            set {

            }
            get {
                return value;
            }
        }
        public int CompareTo(object obj)
        {
            SortableObject intObj = obj as SortableObject;
            if (intObj != null) {
                if (this.value < intObj.value)
                {
                    return -1;
                }
                else if (this.value > intObj.value)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            return -2;

        }

    }
}

