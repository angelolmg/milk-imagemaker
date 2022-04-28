public class LabelingInfo
{
    public int index;
    public float x, y, width, height;

    public LabelingInfo(int mindex, float mx, float my, float mwidth, float mheight){
        index = mindex + 15;
        x = mx;
        y = my;
        width = mwidth;
        height = mheight;
    }
}
