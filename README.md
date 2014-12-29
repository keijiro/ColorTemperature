ColorTemperature
----------------

This is an implementation of a color temperature curve function which runs on
Unity. It could be used to calculate a white point for color balance operations.

![screenshot](http://keijiro.github.io/ColorTemperature/screenshot2.png)

The implementation is based on [the analytical model of the chromaticity of
the daylight illuminants by Judd et al.][Wikipedia] It was slightly modified
to adjust the curve with the D65 white point.

[Wikipedia]: http://en.wikipedia.org/wiki/Standard_illuminant#Illuminant_series_D
