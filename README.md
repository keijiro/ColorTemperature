ColorTemperature
----------------

This is an implementation of a color temperature to RGB converter which runs on
Unity. It takes a color temperature value in Kelvin and returns a corresponding
color.

![screenshot](http://keijiro.github.io/ColorTemperature/screenshot.png)

The implementation is based on the [Tanner Helland's approximation][Helland]
and slightly modified for usability reasons. It might be accurate enough for
graphic applications but not suited to scientific programming.

[Helland]: http://www.tannerhelland.com/4435/convert-temperature-rgb-algorithm-code/
