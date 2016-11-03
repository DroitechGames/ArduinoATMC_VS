int Second = 0;
int Second2 = 0;
int Minute = 0;
int Hour = 0;
int Day = 0;
//
long P_MS = 0;    // 0 MS
long I_MS = 1000; // 1000MS = 1S
//
#define LED 13
#define LED 7
const int LP7 = 7;
const int LP13 = 13;
int L7 = LOW;
int L13 = HIGH;
//
void setup() {
 Serial.begin(9600);
 pinMode(LED,OUTPUT);
}
void loop() {
  unsigned long CurrentMS = millis();
  if (CurrentMS - P_MS > I_MS)
  {
    P_MS = CurrentMS;
    if (P_MS == CurrentMS)
    {
      // Print Time Every Second
        Serial.println(("T: " + String(Second)+ "| S: " +String(Second2) + "| M: "+ String(Minute) + "| H:" + String(Hour)));
        Second++;
        Second2++;
        //
        if (L13 == LOW && L7 == LOW)
        {
        L13 = HIGH;
        L7 = HIGH;
        digitalWrite(LP13, L13);
        delay(150);
        digitalWrite(LP7,L7);
        }
        else
        {
        L13 = LOW;
        L7 = LOW;
        digitalWrite(LP13, L13);
        delay(150);
        digitalWrite(LP7, L7);
        }
        //
      if (Second == 1)
      {
        Second = 0;
        L13 = HIGH;
        digitalWrite(LP13,L13);
        delay(25);
        L7 = LOW;
        digitalWrite(LP7,L7);
        delay(50);
        L13 = LOW;
        digitalWrite(LP13,L13);
        delay(125);
        L7 = HIGH;
        digitalWrite(LP7,L7);
        delay(125);
        L13 = HIGH;
        digitalWrite(LP13,L13);
        delay(25);
        L7 = LOW;
        digitalWrite(LP7,L7);
        delay(25);
        L13 = LOW;
        digitalWrite(LP13,L13);
        delay(50);
        L7 = HIGH;
        digitalWrite(LP7,L7);
        delay(25);
        L7 = LOW;
        digitalWrite(LP7,L7);
      }
      if (Second2 == 60)
      {
        Minute++;
        Second2 = 0;
      }
      if (Minute == 60)
      {
        Hour++;
        Minute = 0;
      }
      if (Hour == 24)
      {
        Day++;
        Hour = 0;
      }
    }
  }
}