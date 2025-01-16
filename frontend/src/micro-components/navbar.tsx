import { Box, Flex, Text, Stack, useBreakpointValue } from "@chakra-ui/react";
import React from "react";
import { useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";

const NavBar = () => {
  const roleId = useSelector((state): any => state.user.roleId);
  const navigate = useNavigate();

  return (
    <Box>
      <Flex
        bg={"white"}
        color={"gray.600"}
        minH={"60px"}
        py={{ base: 2 }}
        px={{ base: 4 }}
        borderBottom={1}
        borderStyle={"solid"}
        borderColor={"gray.200"}
        align={"center"}
      >
        <Flex
          flex={{ base: 1, md: "auto" }}
          ml={{ base: -2 }}
          display={{ base: "flex", md: "none" }}
        ></Flex>
        <Flex flex={{ base: 1 }} justify={{ base: "center", md: "start" }}>
          <Text
            textAlign={useBreakpointValue({ base: "center", md: "left" })}
            fontFamily={"heading"}
            color={"gray.800"}
          >
            Shopify
          </Text>

          <Flex display={{ base: "none", md: "flex" }} ml={10}>
            <DesktopNav roleId={roleId} navigate={navigate} />
          </Flex>
        </Flex>

        <Flex justify={"flex-end"} direction={"row"} spacing={6}>
          <Text>Hello, user!</Text>
        </Flex>
      </Flex>
    </Box>
  );
};

const DesktopNav = ({roleId, navigate}) => {
  const linkColor = "gray.600";
  const linkHoverColor = "gray.800";

  return (
    <Stack direction={"row"} spacing={4}>
      {navItems.map((ni) =>
        roleId === 1 && ni.name === "Admin" ? (
          <React.Fragment></React.Fragment>
        ) : (
          <Box key={ni.name} onClick={() => navigate(ni.path)} cursor="pointer">
            <Box
              as="a"
              p={2}
              fontSize={"sm"}
              fontWeight={500}
              color={linkColor}
              _hover={{
                textDecoration: "none",
                color: linkHoverColor,
              }}
            >
              {ni.name}
            </Box>
          </Box>
        )
      )}
    </Stack>
  );
};

interface NavItem {
  name: string,
  path: string
}
const navItems: NavItem[] = [
  {
    name: "Products",
    path: "/home"
  },
  {
    name: "My Cart",
    path: "/cart"
  },
  {
    name: "History",
    path: "/history"
  },
  {
    name: "Admin",
    path: "/admin"
  },
];

export default NavBar;
