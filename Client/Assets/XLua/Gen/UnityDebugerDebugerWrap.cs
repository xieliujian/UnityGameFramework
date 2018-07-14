#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class UnityDebugerDebugerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(UnityDebuger.Debuger);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 8, 8, 8);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "Init", _m_Init_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Internal_Log", _m_Internal_Log_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Internal_LogWarning", _m_Internal_LogWarning_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Internal_LogError", _m_Internal_LogError_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Log", _m_Log_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LogWarning", _m_LogWarning_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LogError", _m_LogError_xlua_st_);
            
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "EnableLog", _g_get_EnableLog);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "EnableTime", _g_get_EnableTime);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "EnableSave", _g_get_EnableSave);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "EnableStack", _g_get_EnableStack);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "LogFileDir", _g_get_LogFileDir);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "LogFileName", _g_get_LogFileName);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Prefix", _g_get_Prefix);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "LogFileWriter", _g_get_LogFileWriter);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "EnableLog", _s_set_EnableLog);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "EnableTime", _s_set_EnableTime);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "EnableSave", _s_set_EnableSave);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "EnableStack", _s_set_EnableStack);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "LogFileDir", _s_set_LogFileDir);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "LogFileName", _s_set_LogFileName);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "Prefix", _s_set_Prefix);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "LogFileWriter", _s_set_LogFileWriter);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "UnityDebuger.Debuger does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Init_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityDebuger.IDebugerConsole>(L, 2)) 
                {
                    string logFileDir = LuaAPI.lua_tostring(L, 1);
                    UnityDebuger.IDebugerConsole console = (UnityDebuger.IDebugerConsole)translator.GetObject(L, 2, typeof(UnityDebuger.IDebugerConsole));
                    
                    UnityDebuger.Debuger.Init( logFileDir, console );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string logFileDir = LuaAPI.lua_tostring(L, 1);
                    
                    UnityDebuger.Debuger.Init( logFileDir );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 0) 
                {
                    
                    UnityDebuger.Debuger.Init(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityDebuger.Debuger.Init!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Internal_Log_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<object>(L, 2)) 
                {
                    string msg = LuaAPI.lua_tostring(L, 1);
                    object context = translator.GetObject(L, 2, typeof(object));
                    
                    UnityDebuger.Debuger.Internal_Log( msg, context );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string msg = LuaAPI.lua_tostring(L, 1);
                    
                    UnityDebuger.Debuger.Internal_Log( msg );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityDebuger.Debuger.Internal_Log!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Internal_LogWarning_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<object>(L, 2)) 
                {
                    string msg = LuaAPI.lua_tostring(L, 1);
                    object context = translator.GetObject(L, 2, typeof(object));
                    
                    UnityDebuger.Debuger.Internal_LogWarning( msg, context );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string msg = LuaAPI.lua_tostring(L, 1);
                    
                    UnityDebuger.Debuger.Internal_LogWarning( msg );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityDebuger.Debuger.Internal_LogWarning!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Internal_LogError_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<object>(L, 2)) 
                {
                    string msg = LuaAPI.lua_tostring(L, 1);
                    object context = translator.GetObject(L, 2, typeof(object));
                    
                    UnityDebuger.Debuger.Internal_LogError( msg, context );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string msg = LuaAPI.lua_tostring(L, 1);
                    
                    UnityDebuger.Debuger.Internal_LogError( msg );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityDebuger.Debuger.Internal_LogError!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Log_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 1&& translator.Assignable<object>(L, 1)) 
                {
                    object obj = translator.GetObject(L, 1, typeof(object));
                    
                    UnityDebuger.Debuger.Log( obj );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string message = LuaAPI.lua_tostring(L, 1);
                    
                    UnityDebuger.Debuger.Log( message );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 0) 
                {
                    
                    UnityDebuger.Debuger.Log(  );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count >= 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaTypes.LUA_TNONE == LuaAPI.lua_type(L, 2) || translator.Assignable<object>(L, 2))) 
                {
                    string format = LuaAPI.lua_tostring(L, 1);
                    object[] args = translator.GetParams<object>(L, 2);
                    
                    UnityDebuger.Debuger.Log( format, args );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 2&& translator.Assignable<UnityDebuger.ILogTag>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    UnityDebuger.ILogTag obj = (UnityDebuger.ILogTag)translator.GetObject(L, 1, typeof(UnityDebuger.ILogTag));
                    string message = LuaAPI.lua_tostring(L, 2);
                    
                    UnityDebuger.Debuger.Log( obj, message );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 1&& translator.Assignable<UnityDebuger.ILogTag>(L, 1)) 
                {
                    UnityDebuger.ILogTag obj = (UnityDebuger.ILogTag)translator.GetObject(L, 1, typeof(UnityDebuger.ILogTag));
                    
                    UnityDebuger.Debuger.Log( obj );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count >= 2&& translator.Assignable<UnityDebuger.ILogTag>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaTypes.LUA_TNONE == LuaAPI.lua_type(L, 3) || translator.Assignable<object>(L, 3))) 
                {
                    UnityDebuger.ILogTag obj = (UnityDebuger.ILogTag)translator.GetObject(L, 1, typeof(UnityDebuger.ILogTag));
                    string format = LuaAPI.lua_tostring(L, 2);
                    object[] args = translator.GetParams<object>(L, 3);
                    
                    UnityDebuger.Debuger.Log( obj, format, args );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityDebuger.Debuger.Log!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LogWarning_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 1&& translator.Assignable<object>(L, 1)) 
                {
                    object obj = translator.GetObject(L, 1, typeof(object));
                    
                    UnityDebuger.Debuger.LogWarning( obj );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string message = LuaAPI.lua_tostring(L, 1);
                    
                    UnityDebuger.Debuger.LogWarning( message );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count >= 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaTypes.LUA_TNONE == LuaAPI.lua_type(L, 2) || translator.Assignable<object>(L, 2))) 
                {
                    string format = LuaAPI.lua_tostring(L, 1);
                    object[] args = translator.GetParams<object>(L, 2);
                    
                    UnityDebuger.Debuger.LogWarning( format, args );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 2&& translator.Assignable<UnityDebuger.ILogTag>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    UnityDebuger.ILogTag obj = (UnityDebuger.ILogTag)translator.GetObject(L, 1, typeof(UnityDebuger.ILogTag));
                    string message = LuaAPI.lua_tostring(L, 2);
                    
                    UnityDebuger.Debuger.LogWarning( obj, message );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count >= 2&& translator.Assignable<UnityDebuger.ILogTag>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaTypes.LUA_TNONE == LuaAPI.lua_type(L, 3) || translator.Assignable<object>(L, 3))) 
                {
                    UnityDebuger.ILogTag obj = (UnityDebuger.ILogTag)translator.GetObject(L, 1, typeof(UnityDebuger.ILogTag));
                    string format = LuaAPI.lua_tostring(L, 2);
                    object[] args = translator.GetParams<object>(L, 3);
                    
                    UnityDebuger.Debuger.LogWarning( obj, format, args );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityDebuger.Debuger.LogWarning!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LogError_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 1&& translator.Assignable<object>(L, 1)) 
                {
                    object obj = translator.GetObject(L, 1, typeof(object));
                    
                    UnityDebuger.Debuger.LogError( obj );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string message = LuaAPI.lua_tostring(L, 1);
                    
                    UnityDebuger.Debuger.LogError( message );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count >= 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaTypes.LUA_TNONE == LuaAPI.lua_type(L, 2) || translator.Assignable<object>(L, 2))) 
                {
                    string format = LuaAPI.lua_tostring(L, 1);
                    object[] args = translator.GetParams<object>(L, 2);
                    
                    UnityDebuger.Debuger.LogError( format, args );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 2&& translator.Assignable<UnityDebuger.ILogTag>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    UnityDebuger.ILogTag obj = (UnityDebuger.ILogTag)translator.GetObject(L, 1, typeof(UnityDebuger.ILogTag));
                    string message = LuaAPI.lua_tostring(L, 2);
                    
                    UnityDebuger.Debuger.LogError( obj, message );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count >= 2&& translator.Assignable<UnityDebuger.ILogTag>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaTypes.LUA_TNONE == LuaAPI.lua_type(L, 3) || translator.Assignable<object>(L, 3))) 
                {
                    UnityDebuger.ILogTag obj = (UnityDebuger.ILogTag)translator.GetObject(L, 1, typeof(UnityDebuger.ILogTag));
                    string format = LuaAPI.lua_tostring(L, 2);
                    object[] args = translator.GetParams<object>(L, 3);
                    
                    UnityDebuger.Debuger.LogError( obj, format, args );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityDebuger.Debuger.LogError!");
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_EnableLog(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, UnityDebuger.Debuger.EnableLog);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_EnableTime(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, UnityDebuger.Debuger.EnableTime);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_EnableSave(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, UnityDebuger.Debuger.EnableSave);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_EnableStack(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, UnityDebuger.Debuger.EnableStack);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LogFileDir(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, UnityDebuger.Debuger.LogFileDir);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LogFileName(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, UnityDebuger.Debuger.LogFileName);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Prefix(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, UnityDebuger.Debuger.Prefix);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LogFileWriter(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, UnityDebuger.Debuger.LogFileWriter);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_EnableLog(RealStatePtr L)
        {
		    try {
                
			    UnityDebuger.Debuger.EnableLog = LuaAPI.lua_toboolean(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_EnableTime(RealStatePtr L)
        {
		    try {
                
			    UnityDebuger.Debuger.EnableTime = LuaAPI.lua_toboolean(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_EnableSave(RealStatePtr L)
        {
		    try {
                
			    UnityDebuger.Debuger.EnableSave = LuaAPI.lua_toboolean(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_EnableStack(RealStatePtr L)
        {
		    try {
                
			    UnityDebuger.Debuger.EnableStack = LuaAPI.lua_toboolean(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LogFileDir(RealStatePtr L)
        {
		    try {
                
			    UnityDebuger.Debuger.LogFileDir = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LogFileName(RealStatePtr L)
        {
		    try {
                
			    UnityDebuger.Debuger.LogFileName = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Prefix(RealStatePtr L)
        {
		    try {
                
			    UnityDebuger.Debuger.Prefix = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LogFileWriter(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    UnityDebuger.Debuger.LogFileWriter = (System.IO.StreamWriter)translator.GetObject(L, 1, typeof(System.IO.StreamWriter));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
