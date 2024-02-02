# EduVision
Leveraging Extended Reality to Make Scientific Experiments Accessible.

## Project Setup

### Software
1. **Clone the GitHub Repo** with CLI:
    1. Open the terminal → navigate to the root directory of your project with: `cd </path/to/repository>`
    2. Run `git clone <GitHub_repository_URL>`

2. **Install Git LFS** with CLI:
    1. In the project (repository)'s root directory, run `git lfs install`
        1. _If there's specific large file types tracked, they should be automatically handled by the `.gitattributes` file_

3. **Open the Project via Unity Hub**
   1. Open Unity Hub → **Add** → select the cloned repository (root directory) → **Add Project**
      1. _It’s recommended that all collaborators use the **same version of Unity**!_ See below for the version that we used.

### Hardware

- 3D printed [box and lid](https://www.tinkercad.com/things/conAYZMl6hU-eduvision-potentiometer-box)
- 3D printed [microscope parts](https://cad.onshape.com/documents/7748fbc7720702ea8f2c18d3/w/c43cdd82785fb79c6b7ef396/e/2c4b4c916f691f39ec568f05?renderMode=0&uiState=65b69d539033a949217c0220)
- 2 potentiometers, ESP32 Microcontroller, wires
- Looking Glass display
- Developer-mode-enabled Android mobile phone

### Software Dependencies
- Unity Editor (version 2022.3.18f LTS)
- Arduino IDE

## Run
1. Connect all hardware components
   - Connect wires to the correct pins & plug into your machine for connection with Arduino IDE.
   - Upload the arduino sketch to the Microcontroller
   - Open the Unity project, connect the Looking Glass display (or mobile phone) + Microcontroller
   - Click play in the unity editor, and use the potentiometers to control.


## Shout-Outs
- Thank you to Looking Glass mentors for help with troubleshooting our hologram camera settings!
- Thank you to all our teammates (Ashley, Trishia, Elizabeth, and Josephine) for being awesome!
- Thank you to our Startup Hack coaches, judges, and additional mentors!

## Contact Information
- [Ashley Neall](https://aneall.github.io/)
- [Trishia Chemaly](https://tchemaly.github.io/)
- [Elizabeth Childs](https://impact.stanford.edu/people/elizabeth-childs)
- [Josephine Miko](https://www.linkedin.com/in/josephine-miko-b77397296/?originalSubdomain=ca)
