import { ListItem, ListItemAvatar, Avatar, ListItemText, TextField, InputAdornment, IconButton } from "@mui/material";
import { useState } from "react";
import { ShoppingCartItem } from "../../../global/models/shoppingCart";
import { Delete, DeleteOutline } from "@mui/icons-material";
import MyTextInput from "../../../components/MyTextInput";
import { Form, Formik, FormikHelpers } from "formik";
import { Product } from "../../../global/models/product";
import * as Yup from 'yup'

interface Props {
  item: ShoppingCartItem;
  key: string
  onRemove: () => void;
  onQuantityChange: (newQuantity: number) => void;
}

const ShoppingCartItemComponent: React.FC<Props> = ({ item, onRemove, onQuantityChange}) => {

  function getMaxQ(): number | undefined {
    if(item.unit === 'kg') {
      return 50
    } else {
      return Number((50 / item.kgPerPcs!).toFixed(0))
    }
  }

  return (
    <ListItem key={item.productId}>
      <ListItemAvatar>
          <Avatar  src={item.productImage} />
      </ListItemAvatar>
      <ListItemText
        primary={item.productName}
        secondary={`${Number(item.totalValue.toFixed(1))} ${item.price.currency} • ${item.price.amount} ${item.price.currency}/${item.price.unit}`}
      />
      <Formik
          validationSchema={Yup.object({quantity: Yup.number()
                                                     .required('Proszę podać ilość')
                                                     .moreThan(0,"Podaj ilość większą niż 0")
                                                     .lessThan(getMaxQ()! + 1, 'Max 50 kg')})}
          initialValues={{quantity: item.quantity}}
          onSubmit={values => onQuantityChange(Number(values.quantity))}
          validateOnMount>
            {({ handleSubmit, isValid}) => (
              <Form className='ui form' onBlur={handleSubmit} onSubmit={handleSubmit} autoComplete='off' style={{width: '100px'}}>
                <MyTextInput 
                inputProps={{ endAdornment: <InputAdornment position="start">{item.unit}</InputAdornment> }} 
                placeholder={"Ilość"} 
                maxValue={getMaxQ()}
                minValue={0}
                showErrors
                type="number"
                name={"quantity"}
                />
              </Form>
            )}
        </Formik>
      <IconButton edge="end" onClick={onRemove}>
          <DeleteOutline color="primary" />
      </IconButton>
    </ListItem>
  );
}

export default ShoppingCartItemComponent;