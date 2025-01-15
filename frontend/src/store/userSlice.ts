import { createSlice } from "@reduxjs/toolkit";

export interface IUser {
  token: string | null;
}

const initialState: IUser = {
  token: null, // Initially no user data
};
const userSlice = createSlice({
  name: "user",
  initialState: initialState,
  reducers: {
    setUserState(state, action) {
      state.token = action.payload;
    },
    clearUser(state) {
      state.token = null;
    },
  },
});

export const { setUserState, clearUser } = userSlice.actions;
export default userSlice.reducer;
