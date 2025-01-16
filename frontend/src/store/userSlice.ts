import { createSlice } from "@reduxjs/toolkit";

export interface IUser {
  token: string | null;
  roleId: number | null;
}

const initialState: IUser = {
  token: null,
  roleId: null
};
const userSlice = createSlice({
  name: "user",
  initialState: initialState,
  reducers: {
    setUserState(state, action) {
      state.token = action.payload.token;
      state.roleId = parseInt(action.payload.roleId)
    },
    clearUser(state) {
      state.token = null;
    },
  },
});

export const { setUserState, clearUser } = userSlice.actions;
export default userSlice.reducer;
