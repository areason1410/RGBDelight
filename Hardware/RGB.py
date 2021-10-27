import serial
import time
ser = serial.Serial("/dev/ttyACM0", 9600)
def led_off():
    ser.write("101,101,101".encode())

def led_on():
    ser.write("255,255,255".encode())

def changeLED(red, green, blue):
    ser.write(f"{red},{green},{blue}".encode());

while(1):
    print(ser.readline());
    ##led_off();
    r = input("Red: ");
    g = input("Green: ");
    b = input("Blue: ");
    changeLED(r, g, b);

#import serial
