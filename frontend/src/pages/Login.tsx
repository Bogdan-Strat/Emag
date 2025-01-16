import { Flex, Input, Heading, Text } from "@chakra-ui/react";
import React, { useEffect, useState } from "react";
import { useColorMode } from "../components/ui/color-mode";
import { Field } from "../components/ui/field";
import { Button } from "../components/ui/button";
import { useNavigate } from "react-router-dom";
import { postWithoutAuth } from "../utils/httpRequests.ts";
import { USER_PORT } from "../environment-variables.ts";
import { useDispatch, useSelector } from "react-redux";
import { setUserState } from "../store/userSlice.ts";

const Login = () => {
  const { toggleColorMode } = useColorMode();
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const token = useSelector((state): any => state.user.token);
  const [user, setUser] = useState({
    email: "",
    password: "",
  });

  useEffect(() => {
    toggleColorMode();
  }, []);

  const login = async () => {
    const data = await postWithoutAuth(USER_PORT, "user/login", user);

    if (data === 0) return;

    const state = {
        token: data.token,
        roleId: data.roleId
    }
    dispatch(setUserState(state));
    navigate("/Home")
  };

  return (
    <Flex
      marginTop="145px"
      width="450px"
      align="center"
      justify="center"
      gap="3"
      direction="column"
      shadow="lg"
      rounded="lg"
      mx="auto"
      p="10"
    >
      <Heading>Welcome back!</Heading>
      <Text width="400px" textAlign="center">
        Shopify is offering a seamless and enjoyable shopping experience with a
        wide range of products.
      </Text>
      <Field width="400px" label="Email" required>
        <Input
          value={user.email}
          onChange={(e) => setUser({ ...user, email: e.target.value })}
          placeholder="Enter your email"
        />
      </Field>
      <Field width="400px" label="Password" required>
        <Input
          value={user.password}
          onChange={(e) => setUser({ ...user, password: e.target.value })}
          type="password"
          placeholder="Enter your password"
        />
      </Field>
      <Button
        onClick={() => login()}
        width="125px"
        rounded="7px"
        colorPalette="blue"
      >
        Login
      </Button>
      <Flex direction="row" gap="2">
        <Text>Don't have an account?</Text>
        <Text
          onClick={() => navigate("Register")}
          color="blue"
          _hover={{ textDecoration: "underline" }}
          cursor="pointer"
        >
          Register here
        </Text>
      </Flex>
    </Flex>
  );
};

export default Login;
