# Installation

Make sure you have .NET 8.0 installed on your PC. If not, please find the download here: https://dotnet.microsoft.com/en-us/download/dotnet/8.0

Then, download and install PTsliCR using the Releases tab on the right side of this GitHub page.

# What does it do and export

PTsliCR cleans PTCR scans by using the precursor isolation window and the possible charges supplied by the user to calculate mass-range windows for potential charge-reduced ions. The intensities of all m/z values outside of these ranges are set to 0.

The output includes:
- `PTCR_cleaned_for_extraction.txt`: a file that is directly compatible with IsoTrac for deconvolution of the masses. Please see XXX for information on IsoTrac.
- `PTCR_cleaned_mgf.txt`: a cleaned .mgf file with only the PTCR scans, and only intensity values retained for PTCR mass ranges.
- `PTCR_cleaned_spectra.pdf`: all the extracted scans, where the top shows the raw spectrum, the bottom the cleaned spectrum, and the pink areas represent the calculated mass ranges where charge-reduced ions may be present.
- `PTCR_cleaned_spectra_summed_PTCR_slice_intensities.png`: a histogram of the summed intensities in all PTCR slices.
- `PTCR_cleaned_spectra_summed_PTCR_slice_intensities.txt`: the values corresponding to the histogram.
# Input files

The input for PTsliCR are .raw files that contain PTCR scans collected in profile mode. It will extract all scans containing the “PTR” scan header tag that Thermo instruments automatically add the PTCR scans. All other scan types are ignored.

# Using PTsliCR

Starting PTsliCR opens up the GUI. The settings are:

- `Input`: select the .raw file.
- `Output`: select an output folder.
- `Precursor charge range`: set the expected charge range of the precursor ion. For example, if this is set to 9–10, PTsliCR will calculate the charge-reduced mass windows for precursors with charge 9 and 10, including all reduced charge states down to 1.
- `Isolation width (Th)`: the isolation width used.
- `Add m/z margin`: add a m/z margin to the edges of the isolation window. The left value is subtracted from the lower m/z boundary, and the right value is added to the higher bound m/z value. If the isolation window is set to 10, and the margins are set to 1 and 1, the precursor isolation window at m/z 1000 will be corrected to m/z 994-1006.

Hit `Start` after the settings are set to run PTsliCR. The progress will be shown in the bottom left.

# Questions, feedback, missing capabilities

Please use the Issues tab on Github or send an email to tveth@uw.edu
