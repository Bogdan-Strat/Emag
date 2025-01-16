import { Flex, Input, Heading, Text } from "@chakra-ui/react";
import React, { useEffect, useState } from "react";
import { useColorMode } from "../components/ui/color-mode";
import { Field } from "../components/ui/field";
import { Button } from "../components/ui/button";
import { useNavigate } from "react-router-dom";
import { postWithoutAuth } from "../utils/httpRequests.ts";
import { USER_PORT } from "../environment-variables.ts";

const Register = () => {
  const { toggleColorMode } = useColorMode();
  const navigate = useNavigate();
  const [user, setUser] = useState({
    email: "",
    password: "",
  });

  useEffect(() => {
    toggleColorMode();
  }, []);

  const register = async () => {
    const data = await postWithoutAuth(USER_PORT, "user/register", user, false);

    if (data === 0) return;

    navigate("/");
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
      <Heading>Welcome to Shopify!</Heading>
      <Text width="400px" textAlign="center">
        Create an account in order to have a seamless and enjoyable shopping
        experience with a wide range of products.
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
        width="125px"
        rounded="7px"
        colorPalette="blue"
        onClick={() => register()}
      >
        Register
      </Button>
      <Flex direction="row" gap="2">
        <Text>Already have an account?</Text>
        <Text
          onClick={() => navigate("/")}
          color="blue"
          _hover={{ textDecoration: "underline" }}
          cursor="pointer"
        >
          Login here
        </Text>
      </Flex>
    </Flex>
  );
};

export default Register;
