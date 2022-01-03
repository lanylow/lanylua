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
  int64_t time = luaL_checkinteger(state, 1);
  Sleep((unsigned long)time);
  return 1;
}

int create_process(lua_State* state) {
  const char* path = luaL_checkstring(state, 1);
  
  STARTUPINFO startup_info = { 0 };
  PROCESS_INFORMATION process_info = { 0 };

  int64_t res = CreateProcessA(path, NULL, NULL, NULL, FALSE, 0, NULL, NULL, &startup_info, &process_info);
  CloseHandle(process_info.hThread);

  int64_t process_id = (int64_t)process_info.dwProcessId;
  int64_t handle = (int64_t)process_info.hProcess;

  lua_pushinteger(state, res);
  lua_pushinteger(state, process_id);
  lua_pushinteger(state, handle);

  return 3;
}

int get_module_by_name(lua_State* state) {
  int64_t process_id = luaL_checkinteger(state, 1);
  const char* module = luaL_checkstring(state, 2);

  HANDLE snapshot = CreateToolhelp32Snapshot(TH32CS_SNAPMODULE | TH32CS_SNAPMODULE32, (unsigned long)process_id);
  MODULEENTRY32 module_entry;
  int64_t res = 0;

  module_entry.dwSize = sizeof(module_entry);

  if (Module32First(snapshot, &module_entry)) {
    do {
      if (!_stricmp(module_entry.szModule, module)) {
        res = (int64_t)module_entry.modBaseAddr;
        break;
      }
    } while (Module32Next(snapshot, &module_entry));
  }

  CloseHandle(snapshot);

  lua_pushinteger(state, res);

  return 1;
}

int read_word(lua_State* state) {
  int64_t handle = luaL_checkinteger(state, 1);
  int64_t address = luaL_checkinteger(state, 2);
  uint16_t buff = 0;

  ReadProcessMemory((HANDLE)handle, (void*)address, &buff, 2, NULL);

  lua_pushinteger(state, (int64_t)buff);

  return 1;
}

int read_dword(lua_State* state) {
  int64_t handle = luaL_checkinteger(state, 1);
  int64_t address = luaL_checkinteger(state, 2);
  uint32_t buff = 0;

  ReadProcessMemory((HANDLE)handle, (void*)address, &buff, 4, NULL);

  lua_pushinteger(state, (int64_t)buff);

  return 1;
}

int read_int(lua_State* state) {
  int64_t handle = luaL_checkinteger(state, 1);
  int64_t address = luaL_checkinteger(state, 2);
  int32_t buff = 0;

  ReadProcessMemory((HANDLE)handle, (void*)address, &buff, 4, NULL);

  lua_pushinteger(state, (int64_t)buff);

  return 1;
}

int read_qword(lua_State* state) {
  int64_t handle = luaL_checkinteger(state, 1);
  int64_t address = luaL_checkinteger(state, 2);
  uint64_t buff = 0;

  ReadProcessMemory((HANDLE)handle, (void*)address, &buff, 8, NULL);

  lua_pushinteger(state, (int64_t)buff);

  return 1;
}

int write_word(lua_State* state) {
  int64_t handle = luaL_checkinteger(state, 1);
  int64_t address = luaL_checkinteger(state, 2);
  uint16_t value = (uint16_t)luaL_checkinteger(state, 3);

  WriteProcessMemory((HANDLE)handle, (void*)address, &value, 2, NULL);

  return 1;
}

int write_dword(lua_State* state) {
  int64_t handle = luaL_checkinteger(state, 1);
  int64_t address = luaL_checkinteger(state, 2);
  uint32_t value = (uint32_t)luaL_checkinteger(state, 3);

  WriteProcessMemory((HANDLE)handle, (void*)address, &value, 4, NULL);

  return 1;
}

int write_qword(lua_State* state) {
  int64_t handle = luaL_checkinteger(state, 1);
  int64_t address = luaL_checkinteger(state, 2);
  uint64_t value = (uint64_t)luaL_checkinteger(state, 3);

  WriteProcessMemory((HANDLE)handle, (void*)address, &value, 8, NULL);

  return 1;
}

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

  lua_register(state, "create_process", create_process);
  lua_register(state, "get_module_by_name", get_module_by_name);
  lua_register(state, "read_word", read_word);
  lua_register(state, "read_dword", read_dword);
  lua_register(state, "read_qword", read_qword);
  lua_register(state, "write_word", write_word);
  lua_register(state, "write_dword", write_dword);
  lua_register(state, "write_qword", write_qword);

  printf("[LANYLUA] running file %s\n", input_file);
  luaL_dofile(state, input_file);

  return 0;
}