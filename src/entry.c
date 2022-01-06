#include <lua/lua.h>
#include <lua/lualib.h>
#include <lua/lauxlib.h>

#include <stdlib.h>
#include <stdint.h>
#include <windows.h>
#include <tlhelp32.h>

int get_executor_version(lua_State* state) {
  lua_pushstring(state, "lanylua v0.1A");
  return 1;
}

int sleep(lua_State* state) {
  lua_Integer time = luaL_checkinteger(state, 1);
  Sleep((unsigned long)time);
  return 1;
}

int is_process_running(lua_State* state) {
  lua_Integer handle = luaL_checkinteger(state, 1);
  unsigned long exit_code = 0;
  GetExitCodeProcess((HANDLE)handle, &exit_code);
  lua_pushboolean(state, exit_code == STILL_ACTIVE);
  return 1;
}

int get_async_key_state(lua_State* state) {
  lua_Integer key = luaL_checkinteger(state, 1);
  lua_pushinteger(state, GetAsyncKeyState((int)key));
  return 1;
}

int create_process(lua_State* state) {
  const char* path = luaL_checkstring(state, 1);
  
  STARTUPINFO startup_info = { 0 };
  PROCESS_INFORMATION process_info = { 0 };

  lua_Integer res = CreateProcessA(path, NULL, NULL, NULL, FALSE, 0, NULL, NULL, &startup_info, &process_info);
  CloseHandle(process_info.hThread);

  lua_Integer process_id = (lua_Integer)process_info.dwProcessId;
  lua_Integer handle = (lua_Integer)process_info.hProcess;

  lua_pushinteger(state, res);
  lua_pushinteger(state, process_id);
  lua_pushinteger(state, handle);

  return 3;
}

int get_module_by_name(lua_State* state) {
  lua_Integer process_id = luaL_checkinteger(state, 1);
  const char* module = luaL_checkstring(state, 2);

  HANDLE snapshot = CreateToolhelp32Snapshot(TH32CS_SNAPMODULE | TH32CS_SNAPMODULE32, (unsigned long)process_id);
  MODULEENTRY32 module_entry;
  lua_Integer res = 0;

  module_entry.dwSize = sizeof(module_entry);

  if (Module32First(snapshot, &module_entry)) {
    do {
      if (!_stricmp(module_entry.szModule, module)) {
        res = (lua_Integer)module_entry.modBaseAddr;
        break;
      }
    } while (Module32Next(snapshot, &module_entry));
  }

  CloseHandle(snapshot);

  lua_pushinteger(state, res);

  return 1;
}

int get_process_id_by_name(lua_State* state) {
  const char* name = luaL_checkstring(state, 1);

  HMODULE snapshot = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
  PROCESSENTRY32 process_entry;
  lua_Integer res = 0;

  process_entry.dwSize = sizeof(process_entry);

  if (Process32First(snapshot, &process_entry)) {
    do {
      if (!_stricmp(process_entry.szExeFile, name)) {
        res = (lua_Integer)process_entry.th32ProcessID;
        break;
      }
    } while (Process32Next(snapshot, &process_entry));
  }

  CloseHandle(snapshot);

  lua_pushinteger(state, res);

  return 1;
}

int close_handle(lua_State* state) {
  lua_Integer handle = luaL_checkinteger(state, 1);
  CloseHandle((HANDLE)handle);
  return 1;
}

int open_process(lua_State* state) {
  lua_Integer process_id = luaL_checkinteger(state, 1);
  lua_pushinteger(state, (lua_Integer)OpenProcess(PROCESS_ALL_ACCESS, FALSE, (unsigned long)process_id));
  return 1;
}

#define READ_FUNCTION(buff_type, buff_size, name) \
int name(lua_State* state) { \
  lua_Integer handle = luaL_checkinteger(state, 1); \
  lua_Integer address = luaL_checkinteger(state, 2); \
  buff_type buff = 0; \
  ReadProcessMemory((HANDLE)handle, (void*)address, &buff, buff_size, NULL); \
  lua_pushinteger(state, (lua_Integer)buff); \
  return 1; \
}

READ_FUNCTION(uint16_t, 2, read_word)
READ_FUNCTION(uint32_t, 4, read_dword)
READ_FUNCTION(uint64_t, 8, read_qword)

READ_FUNCTION(int16_t, 2, read_short)
READ_FUNCTION(int32_t, 4, read_int)
READ_FUNCTION(int64_t, 8, read_int64)

#undef READ_FUNCTION

#define WRITE_FUNCTION(buff_type, buff_size, name) \
int name(lua_State* state) { \
  lua_Integer handle = luaL_checkinteger(state, 1); \
  lua_Integer address = luaL_checkinteger(state, 2); \
  buff_type value = (buff_type)luaL_checkinteger(state, 3); \
  WriteProcessMemory((HANDLE)handle, (void*)address, &value, buff_size, NULL); \
  return 1; \
}

WRITE_FUNCTION(uint16_t, 2, write_word)
WRITE_FUNCTION(uint32_t, 4, write_dword)
WRITE_FUNCTION(uint64_t, 8, write_qword)

WRITE_FUNCTION(int16_t, 2, write_short)
WRITE_FUNCTION(int32_t, 4, write_int)
WRITE_FUNCTION(int64_t, 8, write_int64)

#undef WRITE_FUNCTION

int main(int argc, char** argv) {
  if (*++argv == NULL) {
    fprintf(stderr, "[LANYLUA] ERROR: no input file provided\n");
    exit(1);
  }

  char* input_file = *argv;

  printf("[LANYLUA] creating new lua state\n");
  lua_State* state = luaL_newstate();

  if (state == NULL) {
    fprintf(stderr, "[LANYLUA] ERROR: failed to create new lua state\n");
    exit(1);
  }

  printf("[LANYLUA] loading lua standard libraries\n");
  luaL_openlibs(state);

  printf("[LANYLUA] registering functions\n");
  lua_register(state, "get_executor_version", get_executor_version);
  lua_register(state, "sleep", sleep);
  lua_register(state, "is_process_running", is_process_running);
  lua_register(state, "get_async_key_state", get_async_key_state);

  lua_register(state, "create_process", create_process);
  lua_register(state, "get_module_by_name", get_module_by_name);
  lua_register(state, "get_process_id_by_name", get_process_id_by_name);
  lua_register(state, "close_handle", close_handle);
  lua_register(state, "open_process", open_process);

  lua_register(state, "read_word", read_word);
  lua_register(state, "read_dword", read_dword);
  lua_register(state, "read_qword", read_qword);
  lua_register(state, "read_short", read_short);
  lua_register(state, "read_int", read_int);
  lua_register(state, "read_int64", read_int64);

  lua_register(state, "write_word", write_word);
  lua_register(state, "write_dword", write_dword);
  lua_register(state, "write_qword", write_qword);
  lua_register(state, "write_short", write_short);
  lua_register(state, "write_int", write_int);
  lua_register(state, "write_int64", write_int64);

  printf("[LANYLUA] running file %s\n", input_file);
  luaL_dofile(state, input_file);

  return 0;
}