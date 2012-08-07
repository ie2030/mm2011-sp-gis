@echo off
del GoogleMaps.swf
mxmlc GoogleMaps.mxml -library-path+=map_flex_1_20.swc  > log.txt 2>&1
