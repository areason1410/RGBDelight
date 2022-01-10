//for strcmp check if == -10

// which pins we are using
uint8_t rPin = 13;
uint8_t gPin = 12;
uint8_t bPin = 11;
// forward declration of our functions
void changeLedColours(uint8_t rVal, uint8_t gVal, uint8_t bVal);
void checkInputs();


// setup function for our pins and serial
void setup() 
{
    pinMode(rPin, OUTPUT);
    pinMode(gPin, OUTPUT);
    pinMode(bPin, OUTPUT);
    Serial.begin(9600);
    Serial.println("WE ON");
}

// main system loop, most important stuff is done here
void loop() 
{
  checkInputs();
  delay(1);
                                                                                                                                                                        
}

// change the rgb colours for the bulb, takes values from 0 to 255
void changeLedColours(uint8_t rVal, uint8_t gVal, uint8_t bVal)
{
    analogWrite(rPin, constrain(255-rVal, 0, 255));
    analogWrite(gPin, constrain(255-gVal, 0, 255));
    analogWrite(bPin, constrain(255-bVal, 0, 255));
}

// Checking if the serial has any inputs, and if it does call the changeLedColours() function
// We can do this as we are only going to expect a valid input in the form of "number, number, number"
// (where 0 >= number >= 255).
void checkInputs()
{
    uint8_t temp[3]{};
    while(Serial.available() > 0) 
    {
      changeLedColours(Serial.parseInt(), Serial.parseInt(), Serial.parseInt());
      Serial.readStringUntil("\n"); // for wiping the buffer so it doesnt send 0 to the parse ints :)
      
    }
    
    //while(Serial.available() > 0) 
    //{
     // temp = Serial.readStringUntil("\n");
   // }

   // if(temp.length() < 11) { return; }
    //else { changeLedColours(temp.substring(0, 3).toInt(),temp.substring(4, 7).toInt(),temp.substring(8,11).toInt()); }
  
}
