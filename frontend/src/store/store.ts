import { configureStore } from "@reduxjs/toolkit";
import userReducer from "./userSlice.ts";
import { persistStore, persistReducer } from "redux-persist";
import storage from "redux-persist/lib/storage";

const persistConfig = {
  key: "root",
  storage,
};

const persistedReducer = persistReducer(persistConfig, userReducer);

const store = configureStore({
  reducer: {
    user: persistedReducer,
  },
});
const persistor = persistStore(store);
// persistor.purge();

export {persistor};
export default store;
