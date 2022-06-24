# Basic PC (Windows) process monitoring and creating ability to create boosting scenarios (if process X detected kill processes A, B, C and set Xes priority, etc.)

In one word: it's like Razer Cortex and similar software, but console and give more performance that consume, average consumption 15mb ram and 0.1% of 4-4 core 3.8 GHz processor.

You must provide 2 files: 1)`target.txt`
                          2)`trigger.txt`

#1)target.txt (List of targets to shutdown on detection of trigger from 2)`trigger.txt`)
Example:
```txt
000 | GoogleDriveFS | C:\Windows\RDMLables\GoogleDrive.lnk
001 | wallpaper32   | D:\Steam\steamapps\common\wallpaper_engine\wallpaper32.exe
002 | steam         | C:\Windows\RDMLables\steam.lnk
```

```txt
000 | GoogleDriveFS | C:\Windows\RDMLables\GoogleDrive.lnk
```

Where:
1.1)`000` - ID;
1.2)`GoogleDriveFS` - name of the process;
1.3)`C:\Windows\RDMLables\GoogleDrive.lnk` - path to `.exe` or other file that execute that process.

#2)trigger.txt (List of trigger processes that will start boost squence)
Example:
```txt
000 | factorio           | 0,1
001 | SpaceEngineers     | 0,1,3
```

```txt
000 | factorio           | 0,1
```

Where:
1.1)`000` - ID;
1.2)`factorio` - name of the process;
1.3)`0,1` IDs of targets (1.1) from `target.txt` (1) 













