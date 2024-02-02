//#include <BluetoothSerial.h>
//BluetoothSerial BTSerial;
int focus = 0, ZoomValue = 0;
unsigned long last_run = 0;
unsigned long prevtime = 0;
void setup() {
  // put your setup code here, to run once:
  //Serial.begin(19200);// Potentiometer values from 0 to 4095
  Serial.begin(9600);
  attachInterrupt(digitalPinToInterrupt(32), movement, FALLING);
  pinMode(33, INPUT);
}

void loop() {
  // put your main code here, to run repeatedly:
  ZoomValue = analogRead(36); 
  unsigned long now = millis();
  if(now - prevtime >   60){
  Serial.flush();
  Serial.print(ZoomValue);
  Serial.print(" ");
  Serial.println(focus);
  focus = 0;
  prevtime = now;
  }



}

void movement(){
  if(millis() - last_run>5){
    if(digitalRead(33)==1){
      focus++;
    }
    if(digitalRead(33)==0){
      focus--;
    }
    last_run = millis();

  }
}
// #include <BluetoothSerial.h>
// #include <BTAddress.h>
// #include <BTScan.h>
// #include <BTAdvertisedDevice.h>

// //BluetoothSerial BTSerial;
// BluetoothSerial SerialBT; // Looks like the name 'SerialBT' is important here

// void setup() {
//   // put your setup code here, to run once:
//   //Serial.begin(19200);
//   SerialBT.begin("eduvision-ESP32");
//   delay(1000);
// }

// void loop() {
//   // put your main code here, to run repeatedly:
//   int sensorValue = analogRead(36); 
//   if(SerialBT.available()){
//     //String dataFromBT = BTSerial.readStringUntil('\n');
//     String dataFromBT = SerialBT.readString();
//     SerialBT.println(sensorValue);
//   }
// }