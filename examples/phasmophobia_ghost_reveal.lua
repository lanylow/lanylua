-- Update these offsets
LevelController_TypeInfo = 63552680;
LevelController_GhostRoom = 0x28;
LevelController_GhostAI = 0x30;
LevelRoom_Name = 0x58;
GhostAI_GhostInfo = 0x38;
GhostInfo_Type = 0x20;
GhostInfo_Age = 0x24;
GhostInfo_IsMale = 0x34;
GhostInfo_Name = 0x38;
GhostInfo_IsShy = 0x48;
String_Length = 0x10;
String_Data = 0x14;

local ghost_type_names = {
  [0] = "Spirit",
  [1] = "Wraith",
  [2] = "Phantom",
  [3] = "Poltergeist",
  [4] = "Banshee",
  [5] = "Jinn",
  [6] = "Mare",
  [7] = "Revenant",
  [8] = "Shade",
  [9] = "Demon",
  [10] = "Yurei",
  [11] = "Oni",
  [12] = "Yokai",
  [13] = "Hantu",
  [14] = "Goryo",
  [15] = "Myling",
  [16] = "Onryo",
  [17] = "The Twins",
  [18] = "Raiju",
  [19] = "Obake",
  [20] = "The Mimic"
}

local ghost_type_evidences = {
  [0] = "EMF Level 5, Spirit Box, Ghost Writing",
  [1] = "EMF Level 5, Spirit Box, D.O.T.S Projector",
  [2] = "Spirit Box, Fingerprints, D.O.T.S Projector",
  [3] = "Spirit Box, Fingerprints, Ghost Writing",
  [4] = "Ghost Orb, Fingerprints, D.O.T.S Projector",
  [5] = "EMF Level 5, Freezing Temperatures, Fingerprints",
  [6] = "Ghost Orb, Spirit Box, Ghost Writing",
  [7] = "Ghost Orb, Freezing Temperatures, Ghost Writing",
  [8] = "EMF Level 5, Freezing Temperatures, Ghost Writing",
  [9] = "Freezing Temperatures, Fingerprints, Ghost Writing",
  [10] = "Ghost Orb, Freezing Temperatures, D.O.T.S Projector",
  [11] = "EMF Level 5, Freezing Temperatures, D.O.T.S Projector",
  [12] = "Ghost Orb, Spirit Box, D.O.T.S Projector",
  [13] = "Ghost Orb, Freezing Temperatures, Fingerprints",
  [14] = "EMF Level 5, Fingerprints, D.O.T.S Projector",
  [15] = "EMF Level 5, Fingerprints, Ghost Writing",
  [16] = "Ghost Orb, Spirit Box, Freezing Temperatures",
  [17] = "EMF Level 5, Spirit Box, Freezing Temperatures",
  [18] = "EMF Level 5, Ghost Orb, D.O.T.S Projector",
  [19] = "EMF Level 5, Ghost Orb, Fingerprints",
  [20] = "Spirit Box, Freezing Temperatures, Fingerprints"
}

-- Code
print("Running in executor " .. get_executor_version());

function error(msg)
  print(msg);
  os.exit(false);
end

process_id = get_process_id_by_name("Phasmophobia.exe");
if (process_id == 0) then error("Couldn't find Phasmophobia process") end
print("Found Phasmophobia process id " .. process_id);

game_assembly = get_module_by_name(process_id, "GameAssembly.dll");
if (game_assembly == 0) then error("Couldn't find GameAssembly.dll") end
print("Found GameAssembly.dll at 0x" .. string.format("%x", game_assembly))

handle = open_process(process_id);
if (handle == 0) then
  print("Couldn't open a handle to the process");
  close_handle(handle);
  os.exit(false);
end

print("Opened a handle to the process");

function read_string(address)
  length = read_int(handle, address + String_Length);
  str = "";

  for i = 0, length * 2, 2 do
    str = str .. string.char(read_char(handle, address + String_Data + i));
  end

  return str;
end

static_fields = read_qword(handle, read_qword(handle, read_qword(handle, game_assembly + LevelController_TypeInfo) + 184));
ghost = read_qword(handle, static_fields + LevelController_GhostAI);
if (ghost == 0) then error("Current ghost pointer is 0, make sure you're in game") end

ghost_info = read_qword(handle, ghost + GhostAI_GhostInfo);
ghost_room = read_qword(handle, static_fields + LevelController_GhostRoom);
room_name = read_string(read_qword(handle, ghost_room + LevelRoom_Name));

type = read_int(handle, ghost_info + GhostInfo_Type);
age = read_int(handle, ghost_info + GhostInfo_Age);
is_male = read_byte(handle, ghost_info + GhostInfo_IsMale);
is_shy = read_byte(handle, ghost_info + GhostInfo_IsShy);
name = read_string(read_qword(handle, ghost_info + GhostInfo_Name));

print("\nGhost info:");
print("  Type: " .. ghost_type_names[type]);
print("  Evidence: " .. ghost_type_evidences[type]);
print("  Age: " .. age);
print("  Male: " .. (is_male == 1 and "Yes" or "No"));
print("  Shy: " .. (is_shy == 1 and "Yes" or "No"));
print("  Name: " .. name);
print("  Room: " .. room_name);

close_handle(handle);