import React, {useState} from 'react'
import PhotoUploadWidget from "../../../components/photo/PhotoUploadWidget";
import {useStore} from "../../../global/stores/store";
import {Button, Stack, Typography} from "@mui/material";
import ProductCreateForm from "./ProductCreateForm";

function CreateProductModal() {
  return (
      <Stack direction={'column'} spacing={3}>
          <Typography textAlign={'center'} variant={'h4'}>Nowy produkt</Typography>
          <ProductCreateForm />
      </Stack>
  )
}

export default CreateProductModal