# simple-xcf-writer
Simple class for writing bare XCF file in C#


# Example usage
```csharp

var bitmap1 = ... // obtain from somewhere reference to System.Drawing.Bitmap
var bitmap2 = ... // obtain from somewhere reference to System.Drawing.Bitmap

var xcfWriter = new XCFWriter();

xcfWriter.AddLayer("Layer1", bitmap1);
xcfWriter.AddLayer("Layer2", bitmap2);

xcfWriter.WriteToFile("output_file.xcf");

// XXX: optionally dispose bitmaps - XCFWriter do not do this


```

