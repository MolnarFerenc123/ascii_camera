# ASCII Camera  

The **ASCII Camera** is a console-based C# application that transforms video frames from your webcam into ASCII art in real-time. The program detects multiple cameras and allows the user to select the desired one.  

## Features  

- Converts webcam footage into ASCII characters in real-time.  
- Supports multiple cameras – choose from the available options.  
- Grayscale conversion with precise character mapping.  

## Requirements  

To run this application, you need the following:  

- **Windows operating system**  
- **.NET Framework or .NET Core** (version 4.5 or higher)  
- **AForge.NET** libraries  

## Installation  

1. **Download the source code**  
   Clone or download the project from the GitHub repository:  
   ```bash
   git clone https://github.com/MolnarFerenc123/ascii_camera.git
   ```  

2. **Set up libraries**  
   Ensure the required **AForge.NET** libraries are available in the project. You can install them via the NuGet package manager.  

3. **Build the project**  
   Open the project in Visual Studio and compile it.  

4. **Run the application**  
   Start the application and follow the instructions displayed in the console.  

## Usage  

1. **Select a camera**  
   The program lists all available cameras. Enter the index of the camera you want to use.  

2. **View ASCII output**  
   The selected camera’s feed is displayed as ASCII art in real time within the console window.  

## Example  

A simple ASCII output from a grayscale image:  

```
@@@@@@@@@@@@%%%@**+=  
%%%%%%******++++++==  
++++:::......:::::  
```  

## Customization  

- **Modify the character set**: Replace the predefined characters (`@`, `%`, `*`, etc.) in the program with your own to customize the ASCII art output.  

## Contributions  

We welcome pull requests and issue reports if you'd like to contribute to this project.  

## License  

This project is available under the [MIT license](LICENSE).  