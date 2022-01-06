-- Update these offsets
LevelController_TypeInfo = 63552680;
LevelController_GhostAI = 0x30;
GhostAI_GhostInfo = 0x38;
GhostInfo_GhostType = 0x20;

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

static_fields = read_qword(handle, read_qword(handle, read_qword(handle, game_assembly + LevelController_TypeInfo) + 184));

ghost = read_qword(handle, static_fields + 0x30);
ghost_info = read_qword(handle, ghost + 0x38);

type = read_int(handle, ghost_info + 0x20);
age = read_int(handle, ghost_info + 0x24);
is_male = read_byte(handle, ghost_info + 0x34);
is_shy = read_byte(handle, ghost_info + 0x48);

print("Type: " .. ghost_type_names[type]);
print("Age: " .. age);
print("Male: " .. is_male);
print("Shy: " .. is_shy);

close_handle(handle);