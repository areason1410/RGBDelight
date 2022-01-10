using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace RGBDelight.Models
{
    class LED : INPC
    {
        //Tuple<byte, byte, byte> leds = new Tuple<byte, byte, byte>(0, 0, 0);
       

        private Tuple<byte, byte, byte> _RGB;
        public Tuple<byte, byte, byte> RGB
        {
            get { return _RGB; }
            set
            {
                _RGB = value;
                OnPropertyChanged(nameof(LED));
            }
        }

        private Guid _id;

        public Guid ID
        {
            get { return _id; }
            set 
            { 
                _id = value;
                OnPropertyChanged(nameof(LED));

            }
        }


        public LED(byte r, byte g, byte b)
        {
            _RGB = new Tuple<byte, byte, byte>(0, 0, 0);
            _id = new Guid();
        }




    }
    //class LED
    //{
    //    private bool _state;

    //    public bool State
    //    {
    //        get { return _state; }
    //        set { _state = value; }
    //    }

    //    private ushort _red;

    //    public ushort Red
    //    {
    //        get { return _red; }
    //        set { _red = value; }
    //    }

    //    private ushort _green;

    //    public ushort Green
    //    {
    //        get { return _green; }
    //        set { _green = value; }
    //    }

    //    private ushort _blue;

    //    public ushort Blue
    //    {
    //        get { return _blue; }
    //        set { _blue = value; }
    //    }

    //    private ObservableCollection<UInt16> _RGB;
    //    public ObservableCollection<UInt16> RGB
    //    {
    //        get { return _RGB; }
    //        set { _RGB = value; }
    //    }

    //    public LED(UInt16 r, UInt16 g, UInt16 b)
    //    {
    //        //_RGB = new ObservableCollection<UInt16>();
    //        //_RGB.Add(r);
    //        //_RGB.Add(b);
    //        //_RGB.Add(b);

    //        _red = r;
    //        _green = g;
    //        _blue = b;
    //    }

    //    public void TurnOff() { _state = false; }
    //    public void TurnOn() { _state = true; }
    //    public void ChangeColour(ushort r, ushort g, ushort b)
    //    {
    //        _red = r;
    //        _green = g;
    //        _blue = b;
    //    }

       
}


