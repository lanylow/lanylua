-- Update these offsets 
dwLocalPlayer = 0xDB558C;
dwForceJump = 0x527A9AC;
m_fFlags = 0x104;

-- Code
print("Running in executor " .. get_executor_version());

process_id = 0;
while (process_id == 0) do
  process_id = get_process_id_by_name("csgo.exe");
  sleep(100);
end

print("Found CS:GO process id " .. process_id);

client_module = 0;
while (client_module == 0) do
  client_module = get_module_by_name(process_id, "client.dll");
  sleep(100);
end

print("Found client module address 0x" .. string.format("%x", client_module))

handle = open_process(process_id);
if (handle == 0) then
  print("Couldn't open a handle to the process");
  close_handle(handle);
  os.exit(false);
end

print("Opened a handle to the process");

function get_local_player()
  return read_dword(handle, client_module + dwLocalPlayer);
end

function get_flags(entity)
  return read_int(handle, entity + m_fFlags);
end

function force_jump()
  write_int(handle, client_module + dwForceJump, 5);
  sleep(10);
  write_int(handle, client_module + dwForceJump, 4);
end

while (true) do
  if (not is_process_running(handle)) then
    close_handle(handle);
    os.exit(true);
  end

  local_player = get_local_player();

  if (local_player ~= 0) then
    if ((get_async_key_state(0x20) ~= 0) and (get_flags(local_player) & (1 << 0))) then
      force_jump();
    end
  end

  sleep(1);
end