#define CLK_PIN 2

#define M1_PIN 3
#define MREQ_PIN 4
#define RD_PIN 5
#define WR_PIN 6
#define IORQ_PIN 7
#define RFSH_PIN 8

const char ADDR_BUS[] = { 22, 24, 26, 28, 30, 32, 34, 36, 38, 40, 42, 44, 46, 48, 50, 52 };
// pin mapping:           D7, D6, D5, D4, D3, D2, D1, D0
const char DATA_BUS[] = { 43, 47, 49, 53, 51, 45, 39, 41 };

void setup()
{
    for(int n = 0; n < 16; n++)
    {
        pinMode(ADDR_BUS[n], INPUT);
    }
    for(int n = 0; n < 8; n++)
    {
        pinMode(DATA_BUS[n], INPUT);
    }
    pinMode(CLK_PIN, INPUT);
    pinMode(M1_PIN, INPUT);
    pinMode(MREQ_PIN, INPUT);
    pinMode(RD_PIN, INPUT);
    pinMode(WR_PIN, INPUT);
    pinMode(IORQ_PIN, INPUT);
    pinMode(RFSH_PIN, INPUT);

    attachInterrupt(digitalPinToInterrupt(CLK_PIN), onClock, RISING);

    Serial.begin(115200);
}

void onClock()
{
    char output[80];

    unsigned int address_l = 0;
    unsigned int address_h = 0;
    unsigned int data = 0;

    char addr_lsb[9] = "        ";
    char addr_msb[9] = "        ";
    char data_bits[9] = "        ";

    // The pins are all ACTIVE LOW
    int pin_m1 = digitalRead(M1_PIN) ? 0 : 1;
    int pin_mreq = digitalRead(MREQ_PIN) ? 0 : 1;
    int pin_rd = digitalRead(RD_PIN) ? 0 : 1;
    int pin_wr = digitalRead(WR_PIN) ? 0 : 1;
    int pin_iorq = digitalRead(IORQ_PIN) ? 0 : 1;
    int pin_rfsh = digitalRead(RFSH_PIN) ? 0 : 1;
    
    for(int n = 0; n < 8; n++)
    {
        int bit = digitalRead(ADDR_BUS[n]) ? 1 : 0;
        addr_msb[n] = bit ? '1' : '0';
        if(pin_rfsh)
        {
            addr_msb[n] = ' ';
        }
        address_h = (address_h << 1) + bit;
    }
    
    for(int n = 8; n < 16; n++)
    {
        int bit = digitalRead(ADDR_BUS[n]) ? 1 : 0;
        addr_lsb[n-8] = bit ? '1' : '0';
        if(pin_rfsh && n == 8)
        {
            addr_lsb[n-8] = ' ';
        }
        address_l = (address_l << 1) + bit;
    }

    for(int n = 0; n < 8; n++)
    {
        int bit = digitalRead(DATA_BUS[n]) ? 1 : 0;
        data_bits[n] = bit ? '1' : '0';
        data = (data << 1) + bit;
    }

    if(!pin_rfsh)
    {
        sprintf(output,
                "%s %s (%02X %02X)    %s (%02X)    %s|%s|%s|%s|%s|%s",
                addr_msb,
                addr_lsb,
                address_h,
                address_l,
                data_bits,
                data,
                pin_m1 ? "M1" : "  ",
                pin_mreq ? "MREQ" : "    ",
                "    ",
                pin_rd ? "RD" : "  ",
                pin_wr ? "WR" : "  ",
                pin_iorq ? "IORQ" : "    ");
    }
    else
    {
        sprintf(output,
                "%s %s (    %1X)    %s (%02X)    %s|%s|%s|%s|%s|%s",
                addr_msb,
                addr_lsb,
                //address_h,
                address_l & 15,
                data_bits,
                data,
                pin_m1 ? "M1" : "  ",
                pin_mreq ? "MREQ" : "    ",
                "RFSH",
                pin_rd ? "RD" : "  ",
                pin_wr ? "WR" : "  ",
                pin_iorq ? "IORQ" : "    ");
    }
    
    Serial.println(output);
}

void loop() { }
