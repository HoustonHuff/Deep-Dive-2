#!/bin/sh
while:
do
	gpioInput=$(eval $1)
	echo "$gpioInput" >> /storage/self/primary/Android/data/com.example.filetest/files/MyFileStorag/SampleFile.txt
	sleep 0.5
done
