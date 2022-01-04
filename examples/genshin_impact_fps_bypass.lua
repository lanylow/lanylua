-- Set these values before running the script
process_path = "";
target_fps = 240;

-- Code
print("Running in executor " .. get_executor_version());

function create_process_safe(name)
  res, id, handle = create_process(name);

  if (res == 0) then
    print("Failed to create Genshin Impact process");
    close_handle(handle);
    os.exit(false);
  end

  return id, handle;
end

process_id, process_handle = create_process_safe(process_path);

print("Succesfully created process GenshinImpact.exe with ID " .. process_id);
print("Waiting for UnityPlayer.dll");

unity_player = 0;
while (unity_player == 0) do
  unity_player = get_module_by_name(process_id, "UnityPlayer.dll");
  sleep(100);
end

print("UnityPlayer.dll found at 0x" .. string.format("%x", unity_player));

some_class = 0;
while (some_class == 0) do
  some_class = read_qword(process_handle, unity_player + 0x1ACD578);
  sleep(100);
end

print("Found target class at 0x" .. string.format("%x", some_class));

while (true) do
  if (not is_process_running(process_handle)) then
    close_handle(process_handle);
    os.exit(true);
  end

  fps_value = read_int(process_handle, unity_player + 0x19870A4);
  vsync_value = read_int(process_handle, some_class + 0x3E8);

  if (fps_value == -1) then
    goto continue;
  end

  if (fps_value ~= target_fps) then
    write_int(process_handle, unity_player + 0x19870A4, target_fps);
  end

  if (vsync_value ~= 0) then
    write_int(process_handle, some_class + 0x3E8, 0);
  end

  ::continue::
  sleep(1000);
end