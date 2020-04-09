Z80 Monitor
===========

A simple monitor for the Z80 µP when running at very slow clock frequencies (about 10Hz or lower) for anyone with a spare Arduino Mega (or clone).  
It can be useful while building your own Z80-based computer (who doesn't?) if you don't have a logic analyzer at hand.

This code was heavily inspired by the (originally) similar code by Ben Eater, which used an Arduino Mega to replace a logic analyzer in [his first video about programming the MOS 6502 CPU](https://www.youtube.com/watch?v=LnzuMJLZRdU).

Given the differences between the 6502 and the Z80 CPUs I decided to slightly alter his code in order to:
* Use an higher serial speed (115.2 kbps)
* Send the data over the serial port only once per clock pulse
* Monitor some other control pins (like M1, RFSH, IORQ, ecc.)
* *Allow caching of last good A15-A7 levels during DRAM RFSH* (**TODO**)

I've written this sketch using Visual Studio Code (because I can't write -and read- code on light themed editors) with the [Microsoft's Arduino extension](https://github.com/Microsoft/vscode-arduino), which works well, although the integrated serial console is still not up to the task of showing *fast* changing data (like 3-4 rows/sec).  
For reading the messages from the Arduino, the Arduino IDE's serial console is still a better choice (and you need the Arduino IDE installed anyway for the extension to work).
You'll find the extension's configuration files inside the .vscode folder already configured for a standard Arduino IDE installation path. Change them according to your needs.
