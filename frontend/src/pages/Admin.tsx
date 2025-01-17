import React, { useEffect, useState } from "react";
import NavBar from "../micro-components/navbar.tsx";
import { useColorMode } from "../components/ui/color-mode";
import { useNavigate } from "react-router-dom";
import { useSelector } from "react-redux";
import {
  Button,
  Flex,
  Heading,
  Input,
  Text,
  Textarea,
  Select,
} from "@chakra-ui/react";
import { Field } from "../components/ui/field";
import {
  NumberInputField,
  NumberInputLabel,
  NumberInputRoot,
} from "../components/ui/number-input";
import { post } from "../utils/httpRequests.ts";
import { GATEWAY_PORT } from "../environment-variables.ts";

const Admin = () => {
  const { toggleColorMode } = useColorMode();
  const navigate = useNavigate();
  const token = useSelector((state): any => state.user.token);
  const [product, setProduct] = useState({
    name: "",
    description: "",
    price: 0,
    quantity: 0,
    categoryId: 0,
  });

  useEffect(() => {
    toggleColorMode();
  }, []);

  const add = async () => {
    await post(GATEWAY_PORT, "products/add", product, token, false);
    navigate("/home");
  }

  return (
    <React.Fragment>
      <NavBar />
      <Flex justify="center" mt="10">
        <Flex direction="column" gap="5" mb="10">
          <Heading>Add a new product to the store</Heading>
          <Field width="400px" label="Name" required>
            <Input
              rounded="xl"
              value={product.name}
              onChange={(e) => setProduct({ ...product, name: e.target.value })}
              placeholder="Enter the name"
            />
          </Field>
          <Field width="400px" label="Description" required>
            <Textarea
              rounded="xl"
              value={product.description}
              onChange={(e) =>
                setProduct({ ...product, description: e.target.value })
              }
              placeholder="Enter the description"
            />
          </Field>
            <Field width="400px" label="Price" required>
              <Input
                rounded="xl"
                value={product.price}
                onChange={(e) =>
                  setProduct({ ...product, price: parseInt(e.target.value) })
                }
                placeholder="Enter the price"
              />
            </Field>
            <Field width="400px" label="Quantity" required>
              <Input
                rounded="xl"
                value={product.quantity}
                onChange={(e) =>
                  setProduct({ ...product, quantity: parseInt(e.target.value) })
                }
                placeholder="Enter the quantity"
              />
            </Field>
          <Button
            width="125px"
            rounded="7px"
            colorPalette="blue"
            alignSelf="center"
            onClick={() => add()}
          >
            Add
          </Button>
        </Flex>
      </Flex>
    </React.Fragment>
  );
};

const categories = [
  {
    label: "Phone",
    value: 0,
  },
  {
    label: "TV",
    value: 1,
  },
  {
    label: "Laptop",
    value: 2,
  },
  {
    label: "PC",
    value: 3,
  },
  {
    label: "Fashion",
    value: 4,
  },
  {
    label: "Gaming",
    value: 5,
  },
  {
    label: "Cosmetics",
    value: 6,
  },
  {
    label: "Food",
    value: 7,
  },
  {
    label: "Auto",
    value: 8,
  },
  {
    label: "Appliances",
    value: 9,
  },
];
export default Admin;
