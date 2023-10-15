import React, {useState} from 'react'
import PhotoUploadWidget from "../../../components/photo/PhotoUploadWidget";
import {useStore} from "../../../global/stores/store";
import {Button, Stack, Typography} from "@mui/material";
import ProductCreateForm from "./ProductCreateForm";

interface Props {
    names: string[]
    setNames: (names: string[]) => void
}

function CreateProductModal({names, setNames}: Props) {
  return (
      <Stack direction={'column'} spacing={3}>
          <Typography textAlign={'center'} variant={'h4'}>Nowy produkt</Typography>
          <ProductCreateForm setNames={setNames}  names={names} />
      </Stack>
  )
}

export default CreateProductModal