#define CLOCK 2

const char ADDR[] = { 22, 24, 26, 28, 30, 32, 34, 36, 38, 40, 42, 44, 46, 48, 50, 52 };
const char DATA[] = { 39, 41, 43, 45, 47, 49, 51, 53 };

void setup()
{
    for(int n = 0; n < 16; n++)
    {
        pinMode(ADDR[n], INPUT);
    }
    for(int n = 0; n < 8; n++)
    {
        pinMode(DATA[n], INPUT);
    }
    pinMode(CLOCK, INPUT);

    attachInterrupt(digitalPinToInterrupt(CLOCK), onClock, RISING);

    Serial.begin(57600);
}

void onClock()
{
    for(int n = 0; n < 16; n++)
    {
        int bit = digitalRead(ADDR[n]) ? 1 : 0;
        Serial.print(bit);
    }
    Serial.print("    ");
    for(int n = 0; n < 8; n++)
    {
        int bit = digitalRead(DATA[n]) ? 1 : 0;
        Serial.print(bit);
    }
    Serial.println();
}

void loop()
{
  // put your main code here, to run repeatedly:
}
